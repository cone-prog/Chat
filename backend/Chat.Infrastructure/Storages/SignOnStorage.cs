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
}
