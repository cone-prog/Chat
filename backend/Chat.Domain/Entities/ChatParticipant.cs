
namespace Chat.Domain.Entities;

public class ChatParticipant
{
    public Guid UserId { get; private set; }

    public Conversation Conversation { get; private set; } = null!;

    public Guid ConversationId { get; private set; }

}
