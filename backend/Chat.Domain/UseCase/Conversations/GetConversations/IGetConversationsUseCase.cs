using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.GetConversations;

public interface IGetConversationsUseCase
{
    Task<List<Conversation>> Execute(CancellationToken cancellationToken);
}
