namespace Chat.Domain.Entities;

public class Message
{
    public string Text { get; private set; } = null!;

    public Guid UserId { get; private set; }

    public Guid ConversationId { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }
}
