namespace Chat.Domain.UseCase.Users.SignOut;

public interface ISignOutStorage
{
    Task RemoveSession(Guid sessionId, CancellationToken cancellationToken);
}