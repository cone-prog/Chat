using Chat.Domain.Abstractions;

namespace Chat.Domain.Entities;

public class Chat : Entity
{
    private readonly List<ChatParticipant> participants = new();

    public IReadOnlyCollection<ChatParticipant> Participant => participants.AsReadOnly();

    private readonly List<Message> messages = new();

    public IReadOnlyCollection<Message> Messages => messages.AsReadOnly();

    private Chat() { }

    public static Chat Create(User user)
    {
        if (user is null) throw new ArgumentException();

        var id = Guid.NewGuid();
        var chat = new Chat
        {
            Id = id,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        chat.participants.Add(ChatParticipant.Create(user.Id, chat.Id));
        return chat;
    }
}
