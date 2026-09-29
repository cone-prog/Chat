using Chat.Domain.Abstractions;

namespace Chat.Domain.Entities;

public class Message : Entity
{
    public string Text { get; private set; } = null!;

    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;

    public DateTimeOffset UpdatedAt { get; private set; }

    private Message() { }

    public static Message Create(User user, string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        if (user is null) throw new ArgumentException();

        if (user.Id == Guid.Empty) throw new ArgumentException();

        var now = DateTimeOffset.UtcNow;
        return new Message
        {
            Id = Guid.NewGuid(),
            CreatedAt = now,
            Text = text,
            UserId = user.Id,
            User = user,
            UpdatedAt = now
        };
    }
}
