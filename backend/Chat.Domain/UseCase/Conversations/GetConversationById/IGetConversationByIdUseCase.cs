using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.GetConversationById;

public interface IGetConversationByIdUseCase
{
    Task<Conversation> Execute(Guid id, CancellationToken cancellationToken);
}
