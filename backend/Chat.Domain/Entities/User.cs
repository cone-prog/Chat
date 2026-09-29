using Chat.Domain.Abstractions;

namespace Chat.Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    private User() { }

    public static User Create(string name, string email, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        return new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTimeOffset.UtcNow,
            Name = name,
            Email = email,
            PasswordHash = passwordHash
        };
    }
}
