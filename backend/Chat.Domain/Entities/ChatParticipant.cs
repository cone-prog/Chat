using Chat.Domain.Abstractions;

namespace Chat.Domain.Entities;

public class ChatParticipant : Entity
{
    public Guid UserId { get; private set; }
    public Guid ChatId { get; private set; }

    private ChatParticipant() { }

    public static ChatParticipant Create(Guid userId, Guid chatId)
    {
        if (userId == Guid.Empty) throw new ArgumentException();

        if (chatId == Guid.Empty) throw new ArgumentException();

        return new ChatParticipant
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTimeOffset.UtcNow,
            UserId = userId,
            ChatId = chatId
        };
    }
}
