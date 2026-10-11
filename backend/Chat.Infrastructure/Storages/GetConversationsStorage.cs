using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.GetConversations;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

public class GetConversationsStorage(
    ChatDbContext dbContext,
    IMapper mapper)
 : IGetConversationsStorage
{
    public async Task<List<Conversation>> GetConversations(Guid userId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Conversations
            .Where(c => c.Participants
                .Any(p => p.UserId == userId))
            .ProjectTo<Domain.Entities.Conversation>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
