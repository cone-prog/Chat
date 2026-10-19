using Chat.Domain.Authentication;
using Chat.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Chat.Domain.UseCase.Users.SignIn;

public class SignInUseCase(
    ISymmetricEncryptor symmetricEncryptor,
    ISignInStorage storage,
    IPasswordManager passwordManager
    ) : IRequestHandler<SignInCommand, (IIdentity identity, string token)>
{
    public async Task<(IIdentity identity, string token)> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var recognisedUser = await storage.FindUser(command.Email, cancellationToken);
        
        if (recognisedUser is null)
        {
            throw new UserNotFoundEcxeption(command.Email);
        }

        if (!passwordManager.ComparePassword(command.Password, recognisedUser.Salt, recognisedUser.PasswordHash))
        {
            throw new UserNotFoundEcxeption(command.Email);
        }
        
        var sessionId = await storage.CreateSession(recognisedUser.Id, 
            DateTimeOffset.UtcNow + TimeSpan.FromHours(1),cancellationToken);
        var token = await symmetricEncryptor.Encrypt(sessionId.ToString(), cancellationToken);

        return (new User(recognisedUser.Id, sessionId), token);
    }
}