namespace Chat.Infrastructure.Models;

public class Conversation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    private readonly List<ChatParticipant> participants = new();

    public IReadOnlyCollection<ChatParticipant> Participants => participants.AsReadOnly();

    private readonly List<Message> messages = new();

    public IReadOnlyCollection<Message> Messages => messages.AsReadOnly();

    public DateTimeOffset CreatedAt { get; set; }
}
