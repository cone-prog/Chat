namespace Chat.Domain.UseCase.Users.SignOn;

public interface ISignOnStorage
{
    Task<Guid> Create(string name,string email, byte[] passwordHash, byte[] salt, CancellationToken cancellationToken);
}