using Chat.Domain.Authentication;
using FluentValidation;
using MediatR;

namespace Chat.Domain.UseCase.Users.SignOn;

internal class SignOnUseCase (
    ISignOnStorage storage,
    IPasswordManager passwordManager
): IRequestHandler<SignOnCommand, IIdentity>
{
    public async Task<IIdentity> Handle(SignOnCommand command, CancellationToken cancellationToken)
    {
        var (salt, passwordHash) = passwordManager.GeneratePasswordParts(command.Password);
        var userId = await storage.CreateUser(command.Name,command.Email, passwordHash, salt, cancellationToken);
        return new User(userId, Guid.Empty);
    }
}