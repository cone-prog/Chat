namespace Chat.Domain.Abstractions;

public abstract class Entity
{
    public required Guid Id { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}
