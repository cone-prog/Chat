namespace Chat.Domain.UseCase.Users.SignOn;

public interface ISignOnStorage
{
    Task<Guid> CreateUser(string name,string email, byte[] passwordHash, byte[] salt, CancellationToken cancellationToken);
    
}