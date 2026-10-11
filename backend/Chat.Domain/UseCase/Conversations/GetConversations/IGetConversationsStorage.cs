using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.GetConversations;

public interface IGetConversationsStorage
{
    Task<List<Conversation>> GetConversations(Guid UserId, CancellationToken cancellationToken);
}
