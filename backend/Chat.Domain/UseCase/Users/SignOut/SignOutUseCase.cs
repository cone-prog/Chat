using Chat.Domain.Authentication;
using MediatR;

namespace Chat.Domain.UseCase.Users.SignOut;

public class SignOutUseCase(
    ISignOutStorage storage,
    IIdentityProvider identityProvider) : IRequestHandler<SignOutCommand>
{
    public async Task Handle(SignOutCommand command, CancellationToken cancellationToken)
    {
        await storage.RemoveSession(identityProvider.Current.SessionId, cancellationToken);
    }
}