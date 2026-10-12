
using Microsoft.Extensions.Options;

namespace Chat.Domain.Authentication;

public class AuthenticationService(ISymmetricDecryptor decryptor,
    IOptions<AuthenticationConfiguration> options) : IAuthenticationService
{
    public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
    {
        var userIdString = await decryptor.Decrypt(authToken, options.Value.Key, cancellationToken);
        return new User(Guid.Parse(userIdString));
    }
}
