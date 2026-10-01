using Chat.Domain.Abstractions;

namespace Chat.Domain.Entities;

public class ChatParticipant : Entity
{
    public User User { get; private set; } = null!;

    public Guid UserId { get; private set; }

    public Conversation Conversation { get; private set; } = null!;

    public Guid ConversationId { get; private set; }

    private ChatParticipant() { }

    public static ChatParticipant Create(User user, Conversation conversation)
    {
        if (user is null) throw new ArgumentException();

        if (conversation is null) throw new ArgumentException();

        if (user.Id == Guid.Empty) throw new ArgumentException();

        if (conversation.Id == Guid.Empty) throw new ArgumentException();

        return new ChatParticipant
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTimeOffset.UtcNow,
            User = user,
            Conversation = conversation,
            UserId = user.Id,
            ConversationId = conversation.Id
        };
    }
}
