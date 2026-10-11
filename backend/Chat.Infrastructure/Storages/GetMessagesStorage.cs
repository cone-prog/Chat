using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Messages.GetMessages;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

public class GetMessagesStorage(ChatDbContext dbContext, IMapper mapper)
    : IGetMessagesStorage
{
    public async Task<IEnumerable<Message>> GetMessagesAsync(Guid conversationId, Guid? beforeMessageId,
        Guid UserId, int limit, CancellationToken cancellationToken)
    {
        var messageBefore = await dbContext.Messages.FindAsync(beforeMessageId,
            cancellationToken);

        if (messageBefore is not null)
            return await dbContext.Messages
                .Where(m => m.CreatedAt < messageBefore.CreatedAt)
                .OrderBy(m => m.CreatedAt)
                .Take(limit)
                .ProjectTo<Domain.Entities.Message>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        return await dbContext.Messages
            .OrderBy(m => m.CreatedAt)
            .Take(limit)
            .ProjectTo<Domain.Entities.Message>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
