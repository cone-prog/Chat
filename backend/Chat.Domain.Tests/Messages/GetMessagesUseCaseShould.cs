using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Messages.GetMessages;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;
using Xunit;

namespace Chat.Domain.Tests.Messages;

public class GetMessagesUseCaseShould
{
    private readonly ISetup<IGetMessagesStorage, Task<IEnumerable<Message>>> getMessagesSetup;
    private readonly ISetup<IIntentionManager, bool> intentionIsAllowedSetup;
    private readonly ISetup<IIdentity, Guid> getCurrentUserIdSetup;
    private readonly Mock<IGetMessagesStorage> storage;
    private readonly GetMessagesUseCase sut;
    private readonly Mock<IIntentionManager> intentionManager;

    public GetMessagesUseCaseShould()
    {
        storage = new Mock<IGetMessagesStorage>();
        getMessagesSetup = storage.Setup(s => s.GetMessagesAsync(It.IsAny<Guid>(), It.IsAny<Guid?>(), It.IsAny<Guid>(),
            It.IsAny<int>(), It.IsAny<CancellationToken>()));

        var validator = new Mock<IValidator<GetMessagesQuery>>();
        validator
            .Setup(v => v.ValidateAsync(It.IsAny<GetMessagesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        intentionManager = new Mock<IIntentionManager>();
        intentionIsAllowedSetup = intentionManager.Setup(i => i.IsAllowed(It.IsAny<MessageIntentions>()));

        var identity = new Mock<IIdentity>();
        var identityProvider = new Mock<IIdentityProvider>();
        identityProvider.Setup(i => i.Current)
            .Returns(identity.Object);
        getCurrentUserIdSetup = identity.Setup(i => i.UserId);


        sut = new GetMessagesUseCase(storage.Object,
           identityProvider.Object, intentionManager.Object);
    }

    [Fact]
    public async Task ReturnsMessages_ExtractedFromConversation_WhenConversationExists()
    {
        var query = new GetMessagesQuery(
            Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
            Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
            30
        );

        var userId = Guid.Parse("550e8400-e29b-41d4-a716-446655440005");
        var expected = new List<Message>()
        {
            new Message()
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
                ConversationId =  query.ConversationId,
                Text = "bla",
                UserId  = userId,
                UpdatedAt = new DateTimeOffset(2020, 12,21,12,12,12, TimeSpan.Zero)
            }
        };

        intentionIsAllowedSetup.Returns(true);
        getMessagesSetup.ReturnsAsync(expected);
        getCurrentUserIdSetup.Returns(userId);

        var actual = await sut.Handle(query, CancellationToken.None);

        Assert.Same(expected, actual);
        storage.Verify(s => s.GetMessagesAsync(query.ConversationId, query.BeforeMessageId, userId,
            30, It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task ThrowIntentionManagerException_WhetGettingNotAllwed()
    {
        var query = new GetMessagesQuery(
            Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
            Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
            30
        );

        var userId = Guid.Parse("550e8400-e29b-41d4-a716-446655440001");


        intentionIsAllowedSetup.Returns(false);

        await Assert.ThrowsAsync<IntentionManagerException>(async () => await sut.Handle(query,
            CancellationToken.None));
        intentionManager.Verify(m => m.IsAllowed(MessageIntentions.Get));
    }
}
