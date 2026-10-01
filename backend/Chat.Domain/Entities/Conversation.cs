using Chat.Domain.Abstractions;

namespace Chat.Domain.Entities;

public class Conversation : Entity
{
    private readonly List<ChatParticipant> participants = new();

    public IReadOnlyCollection<ChatParticipant> Participants => participants.AsReadOnly();

    private readonly List<Message> messages = new();

    public IReadOnlyCollection<Message> Messages => messages.AsReadOnly();

    private Conversation() { }

    public static Conversation Create(User user)
    {
        if (user is null) throw new ArgumentException();

        var id = Guid.NewGuid();
        var conversation = new Conversation
        {
            Id = id,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        conversation.participants.Add(ChatParticipant.Create(user, conversation));
        return conversation;
    }
}
