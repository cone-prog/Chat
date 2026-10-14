using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chat.Domain.UseCase.Users.SignIn;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Storages;

public class SignInStorage (ChatDbContext dbContext, IMapper mapper)
    : ISignInStorage
{
    public async Task<RecognisedUser?> FindUser(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users.Where(u => u.Email.Equals(email))
            .ProjectTo<RecognisedUser>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateSession(Guid userId, DateTimeOffset expiresAt, CancellationToken cancellationToken)
    {
        var newSession = new Session()
        {
            Id = Guid.NewGuid(),
            ExpiresAt = expiresAt,
            UserId = userId
        };

        await dbContext.Sessions.AddAsync(newSession, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return newSession.Id;
    }
}