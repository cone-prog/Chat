using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.GetConversationById;

public interface IGetConversationByIdStorage
{
    Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
