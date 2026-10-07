using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Messages.CreateMessage;

public interface ICreateMessageStorage
{
    Task<Message> CreateAsync(Guid conversationId, Guid userId, string text, CancellationToken cancellationToken);

    Task<bool> ConversationExistsAsync(Guid conversationId, CancellationToken cancellationToken);
}
