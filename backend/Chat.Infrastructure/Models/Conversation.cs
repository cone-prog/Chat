namespace Chat.Infrastructure.Models;

internal class Conversation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public List<ChatParticipant> Participants { get; set; } = null!;

    public List<Message> Messages { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
}
