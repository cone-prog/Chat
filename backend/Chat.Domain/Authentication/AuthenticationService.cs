
namespace Chat.Domain.Authentication;

public class AuthenticationService(ISymmetricDecryptor decryptor) : IAuthenticationService
{
    public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
    {
        var userIdString = await decryptor.Decrypt(authToken,  cancellationToken);
        return new User(Guid.Parse(userIdString));
    }
}
