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
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        var (salt, passwordHash) = passwordManager.GeneratePasswordParts(command.Password);
        var userId = await storage.CreateUser(command.Name,command.Email, passwordHash, salt, cancellationToken);
        var sessionId = await storage.CreateSession(userId, DateTimeOffset.UtcNow + TimeSpan.FromHours(1), cancellationToken);
        return new User(userId, sessionId);
    }
}