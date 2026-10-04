using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.GetConversationById;

public interface IGetCoversationByIdUseCase
{
    Task<Conversation> Execute(Guid id, CancellationToken cancellationToken);
}
