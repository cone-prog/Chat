namespace Chat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public Guid UserId { get; set; }

    public Guid ConversationId { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
