using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

public class GetConversationByIdStorage(ChatDbContext dbContext)
    : IGetConversationByIdStorage
{
    public async Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Conversations
            .Where(c => c.Id == id)
            .Select(
                c => new Conversation
                {
                    Id = c.Id,
                    Name = c.Name
                })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
