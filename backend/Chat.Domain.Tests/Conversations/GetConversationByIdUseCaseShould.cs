using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Domain.UseCase.Conversations.Intentions;
using Moq;
using Moq.Language.Flow;

namespace Chat.Domain.Tests.Conversations;

public class GetConversationByIdUseCaseShould
{
    private readonly ISetup<IGetConversationByIdStorage, Task<Conversation?>> getConversationByidSetup;
    private readonly ISetup<IIdentity, Guid> getCurrentUserIdSetup;
    private readonly ISetup<IIntentionManager, bool> intentionIsAllowedSetup;
    private readonly IGetConversationByIdUseCase sut;
    private readonly Mock<IGetConversationByIdStorage> storage;
    private readonly Mock<IIntentionManager> intentionManager;

    public GetConversationByIdUseCaseShould()
    {
        storage = new Mock<IGetConversationByIdStorage>();
        getConversationByidSetup = storage.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()));

        var identity = new Mock<IIdentity>();
        var identityProvider = new Mock<IIdentityProvider>();
        identityProvider.Setup(p => p.Current).Returns(identity.Object);
        getCurrentUserIdSetup = identity.Setup(i => i.UserId);

        intentionManager = new Mock<IIntentionManager>();
        intentionIsAllowedSetup = intentionManager.Setup(i => i.IsAllowed(It.IsAny<ConversationIntentions>()));

        sut = new GetConversationByIdUseCase(storage.Object, identityProvider.Object, intentionManager.Object);
    }

    [Fact]
    public async Task ThrowNotFoundException_WnenConversationNotExists()
    {
        var conversationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
        getConversationByidSetup.ReturnsAsync((Conversation?)null);
        intentionIsAllowedSetup.Returns(true);

        await Assert.ThrowsAsync<ConversationNotFoundExcetption>(() => sut.Execute(
            conversationId, CancellationToken.None)
        );

        storage.Verify(s => s.GetByIdAsync(conversationId, It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task ThrowIntentionManagerException_WnenConversationGettingIsNotAllowed()
    {
        var conversationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
        intentionIsAllowedSetup.Returns(false);

        await Assert.ThrowsAsync<IntentionManagerException>(() => sut.Execute(
            conversationId, CancellationToken.None)
        );

        storage.Verify(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        intentionManager.Verify(s => s.IsAllowed(ConversationIntentions.GetById));
    }

    [Fact]
    public async Task ReturnConversationWithNeededId()
    {
        getCurrentUserIdSetup.Returns(Guid.Parse("550e8400-e29b-41d4-a716-446655440001"));
        var expected = new Conversation()
        {
            Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            Name = "Some name"
        };

        intentionIsAllowedSetup.Returns(true);
        getConversationByidSetup.ReturnsAsync(expected);

        var actual = await sut.Execute(expected.Id, CancellationToken.None);

        Assert.Same(expected, actual);
        storage.Verify(s => s.GetByIdAsync(expected.Id, It.IsAny<CancellationToken>()));
    }
}
