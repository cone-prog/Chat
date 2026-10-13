using Chat.Domain.Authentication;

namespace Chat.Domain.UseCase.Users.SignIn;

public interface ISignInUseCase
{
    Task<(IIdentity identity, string token)> Execute(SignInCommand command, CancellationToken cancellationToken); 
}