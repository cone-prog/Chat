using Chat.Domain.Authentication;

namespace Chat.Domain.UseCase.Users.SignOn;

public interface ISignOnUseCase
{
    Task<IIdentity> Execute(SignOnCommand command, CancellationToken cancellationToken);
}