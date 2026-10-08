using AutoMapper;
using Chat.Domain.UseCase.Messages.CreateMessage;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

public class CreateMessageStorage(ChatDbContext dbContext, IMapper mapper)
    : ICreateMessageStorage
{
    public async Task<bool> ConversationExistsAsync(Guid conversationId, CancellationToken cancellationToken)
    {
        return await dbContext.Conversations.AnyAsync(c => c.Id == conversationId, cancellationToken);
    }

    public async Task<Domain.Entities.Message> CreateAsync(
            Guid conversationId, Guid userId, string text, CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;
        var message = new Message()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ConversationId = conversationId,
            Text = text,
            UpdatedAt = now,
            CreatedAt = now
        };

        await dbContext.Messages.AddAsync(message, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<Domain.Entities.Message>(message);
    }
}
