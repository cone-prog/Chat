using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Messages.CreateMessage;

public interface ICreateMessageUseCase
{
    Task<Message> Execute(CreateMessageCommand command, CancellationToken cancellationToken);
}
