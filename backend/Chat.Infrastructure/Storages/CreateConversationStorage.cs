using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Models;

namespace Chat.Infrastructure.Storages;

public class CreateConversationStorage(ChatDbContext dbContext) : ICreateConversationStorage
{
    public async Task<Domain.Entities.Conversation> CreateConversationAsync(Guid userId, string name,
        CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;
        var newConversation = new Conversation
        {
            Id = Guid.NewGuid(),
            Name = name,
            CreatedAt = now,
            Participants = new List<ChatParticipant>(),
            Messages = new List<Message>()
        };

        var newParticipant = new ChatParticipant
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Conversation = newConversation,
            CreatedAt = now
        };

        newConversation.Participants.Add(newParticipant);
        await dbContext.AddAsync(newConversation, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new Domain.Entities.Conversation
        {
            Id = newConversation.Id,
            Name = newConversation.Name
        };
    }
}
