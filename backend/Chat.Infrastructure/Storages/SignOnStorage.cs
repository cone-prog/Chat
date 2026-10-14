using Chat.Domain.UseCase.Users.SignOn;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Models;

namespace Chat.Infrastructure.Storages;

public class SignOnStorage(ChatDbContext dbContext) : ISignOnStorage
{
    public async Task<Guid> CreateUser(string name, string email, byte[] passwordHash, byte[] salt, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();

        await dbContext.Users.AddAsync(new User
        {
            Id = userId,
            Email = email,
            CreatedAt = DateTimeOffset.UtcNow,
            Name = name,
            Salt = salt,
            PasswordHash = passwordHash
            
        }, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return userId;
    }
    
    public async Task<Guid> CreateSession(Guid userId, DateTimeOffset expiresAt, CancellationToken cancellationToken)
    {
        var newSession = new Session()
        {
            Id = Guid.NewGuid(),
            ExpiresAt = expiresAt,
            UserId = userId
        };
        
        await dbContext.Sessions.AddAsync(newSession,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return newSession.Id;
    }
}