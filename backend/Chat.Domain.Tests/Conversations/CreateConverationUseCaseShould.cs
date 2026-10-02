using Chat.Domain.Authentication;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Moq;
using Moq.Language.Flow;

namespace Chat.Domain.Tests.Conversations;

public class CreateConverationUseCaseShould
{
    private readonly ISetup<ICreateConversationStorage, Task<Conversation>> createConversationSetup;
    private readonly ICreateConversationUseCase sut;
    private readonly ISetup<IIdentity, Guid> getCurrentUserIdSetup;
    private readonly Mock<ICreateConversationStorage> storage;

    public CreateConverationUseCaseShould()
    {
        storage = new Mock<ICreateConversationStorage>();

        createConversationSetup = storage
            .Setup(s => s.CreateConversationAsync(It.IsAny<Guid>(),
                It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var identity = new Mock<IIdentity>();
        var identityProvider = new Mock<IIdentityProvider>();
        identityProvider.Setup(p => p.Current).Returns(identity.Object);
        getCurrentUserIdSetup = identity.Setup(s => s.UserId);

        sut = new CreateConversationUseCase(storage.Object, identityProvider.Object);
    }

    [Fact]
    public async Task ReturnNewlyCreatedConversation()
    {
        var userId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");

        getCurrentUserIdSetup.Returns(userId);

        var expected = new Conversation();
        createConversationSetup.ReturnsAsync(expected);

        var actual = await sut.Execute("SomeName", CancellationToken.None);

        Assert.Same(expected, actual);

        storage.Verify(s => s.CreateConversationAsync(userId, "SomeName", It.IsAny<CancellationToken>()));
    }
}
