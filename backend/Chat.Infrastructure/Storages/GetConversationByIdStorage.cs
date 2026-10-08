using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

internal class GetConversationByIdStorage(ChatDbContext dbContext, IMapper mapper)
    : IGetConversationByIdStorage
{
    public async Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Conversations
            .Where(c => c.Id == id)
            .ProjectTo<Domain.Entities.Conversation>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
