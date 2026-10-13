namespace Chat.Domain.UseCase.Users.SignIn;

public interface ISignInStorage
{
    Task<RecognisedUser?> FindUser(string email, CancellationToken cancellationToken);
}