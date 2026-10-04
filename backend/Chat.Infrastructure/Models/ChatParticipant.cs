namespace Chat.Infrastructure.Models;

internal class ChatParticipant
{
    public Guid Id { get; set; }

    public User User { get; set; } = null!;

    public Guid UserId { get; set; }

    public Conversation Conversation { get; set; } = null!;

    public Guid ConversationId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
