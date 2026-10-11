using System.ComponentModel.DataAnnotations;

namespace Chat.Infrastructure.Models;

internal class Conversation
{
    public Guid Id { get; set; }

    [MaxLength(70)]
    public string Name { get; set; } = null!;

    public List<ChatParticipant> Participants { get; set; } = null!;

    public List<Message> Messages { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
