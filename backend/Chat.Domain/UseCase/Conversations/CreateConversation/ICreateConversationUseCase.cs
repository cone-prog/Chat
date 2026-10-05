using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

public interface ICreateConversationUseCase
{
    Task<Conversation> Execute(CreateConversationCommand command, CancellationToken cancellationToken);
}
