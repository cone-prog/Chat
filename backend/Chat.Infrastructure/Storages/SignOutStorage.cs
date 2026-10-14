using Chat.Domain.UseCase.Users.SignOut;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

public class SignOutStorage(ChatDbContext dbContext) : ISignOutStorage
{
    public async Task RemoveSession(Guid sessionId, CancellationToken cancellationToken)
    {
        await dbContext.Sessions
            .Where(s => s.Id == sessionId)
            .ExecuteDeleteAsync(cancellationToken);;
    }
}