namespace Chat.Domain.UseCase.Users.SignIn;

public interface ISignInStorage
{
    Task<RecognisedUser?> FindUser(string email, CancellationToken cancellationToken);
    
    Task<Guid> CreateSession(Guid userId, DateTimeOffset expiresAt, CancellationToken cancellationToken);
}