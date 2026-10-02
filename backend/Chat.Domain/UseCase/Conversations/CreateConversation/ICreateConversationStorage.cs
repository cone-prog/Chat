using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

public interface ICreateConversationStorage
{
    Task<Conversation> CreateConversationAsync(Guid userId, string name,
        CancellationToken cancellationToken);
}
