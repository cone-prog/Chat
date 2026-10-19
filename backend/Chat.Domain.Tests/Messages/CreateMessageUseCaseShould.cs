using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.UseCase.Messages.CreateMessage;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;
using Xunit;

namespace Chat.Domain.Tests.Messages;

public class CreateMessageUseCaseShould
{
    private readonly CreateMessageUseCase sut;
    private readonly ISetup<IIntentionManager, bool> intentionIsAllowedSetup;
    private readonly ISetup<IIdentity, Guid> getCurrentUserIdSetup;
    private readonly Mock<ICreateMessageStorage> storage;
    private readonly ISetup<ICreateMessageStorage, Task<Message>> createMessageSetup;
    private readonly ISetup<ICreateMessageStorage, Task<bool>> conversationExistsSetup;

    public CreateMessageUseCaseShould()
    {
        storage = new Mock<ICreateMessageStorage>();

        createMessageSetup = storage.Setup(s => s.CreateAsync(It.IsAny<Guid>(),
            It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        conversationExistsSetup = storage.Setup(s => s.ConversationExistsAsync(It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()));


        var identity = new Mock<IIdentity>();

        var identityProvider = new Mock<IIdentityProvider>();
        identityProvider
            .Setup(i => i.Current)
            .Returns(identity.Object);

        getCurrentUserIdSetup = identity.Setup(i => i.UserId);

        var intentionManager = new Mock<IIntentionManager>();
        intentionIsAllowedSetup = intentionManager.Setup(i => i.IsAllowed(It.IsAny<MessageIntentions>()));

        sut = new CreateMessageUseCase(storage.Object,
            identityProvider.Object, intentionManager.Object);
    }

    [Fact]
    public async Task ReturnNewlyCreatedMessage()
    {
        var command = new CreateMessageCommand(Guid.Parse("550e8400-e29b-41d4-a716-446655440001"), "Something");
        var now = new DateTimeOffset(2025, 12, 12, 12, 0, 0, new TimeSpan(2, 0, 0));
        var userId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
        var expected = new Message()
        {
            UserId = userId,
            ConversationId = command.ConversationId,
            Text = command.Text,
            UpdatedAt = now
        };
        conversationExistsSetup.ReturnsAsync(true);
        getCurrentUserIdSetup.Returns(userId);
        intentionIsAllowedSetup.Returns(true);
        createMessageSetup.ReturnsAsync(expected);

        var actual = await sut.Handle(command, CancellationToken.None);

        Assert.Same(expected, actual);

        storage.Verify(s => s.CreateAsync(command.ConversationId, userId, command.Text, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ThrowConversationNotFoundException_WhenNoMatchingConversation()
    {
        conversationExistsSetup.ReturnsAsync(false);
        var command = new CreateMessageCommand(Guid.Parse("550e8400-e29b-41d4-a716-446655440001"), "Something");

        await Assert.ThrowsAsync<ConversationNotFoundException>(() => sut.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ThrowIntentionManagerException_WhenMessageCreationIsNotAllowed()
    {
        var command = new CreateMessageCommand(Guid.Parse("550e8400-e29b-41d4-a716-446655440001"), "Something");
        conversationExistsSetup.ReturnsAsync(true);
        intentionIsAllowedSetup.Returns(false);

        await Assert.ThrowsAsync<IntentionManagerException>(() => sut.Handle(command, CancellationToken.None));
    }
}
