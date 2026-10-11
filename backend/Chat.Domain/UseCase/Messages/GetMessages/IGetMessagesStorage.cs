using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Messages.GetMessages;

public interface IGetMessagesStorage
{
    Task<IEnumerable<Message>> GetMessagesAsync(Guid conversationId, Guid? beforeMessageId, Guid UserId, int limit,
        CancellationToken cancellationToken);
}
