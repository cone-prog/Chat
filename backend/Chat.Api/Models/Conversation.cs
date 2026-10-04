namespace Chat.Api.Models;

public class Conversation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
}
