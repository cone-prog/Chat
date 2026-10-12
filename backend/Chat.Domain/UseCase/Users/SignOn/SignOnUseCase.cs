using Chat.Domain.Authentication;
using FluentValidation;

namespace Chat.Domain.UseCase.Users.SignOn;

internal class SignOnUseCase (
    IValidator<SignOnCommand> validator,
    ISignOnStorage storage,
    IPasswordManager passwordManager
): ISignOnUseCase
{
    public async Task<IIdentity> Execute(SignOnCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command);
        var (salt, passwordHash) = passwordManager.GeneratePasswordParts(command.Password);
        var userId = await storage.Create(command.Name,command.Email, passwordHash, salt, cancellationToken);
        return new User(userId);
    }
}