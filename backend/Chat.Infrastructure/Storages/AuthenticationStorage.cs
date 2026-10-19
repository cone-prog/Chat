using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chat.Domain.Authentication;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

public class AuthenticationStorage(ChatDbContext dbContext, IMapper mapper) : IAuthenticationStorage
{
    public async Task<Session?> FindSession(Guid sessionId, CancellationToken cancellationToken)
    {
        return await dbContext.Sessions
            .Where(s => s.Id == sessionId)
            .ProjectTo<Session>( mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}