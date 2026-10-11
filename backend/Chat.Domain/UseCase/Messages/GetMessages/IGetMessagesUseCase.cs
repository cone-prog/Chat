using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Messages.GetMessages;

public interface IGetMessagesUseCase
{
    Task<IEnumerable<Message>> Execute(GetMessagesQuery query, CancellationToken cancellationToken);
}
