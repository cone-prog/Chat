namespace Chat.Infrastructure.Models;

public class Message
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public User User { get; set; } = null!;

    public Guid UserId { get; set; }

    public Guid ConversationId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
