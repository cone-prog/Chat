
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace Chat.Domain.Authentication;

public class AuthenticationService(
    ISymmetricDecryptor decryptor, 
    ILogger<AuthenticationService> logger,
    IAuthenticationStorage storage) 
    : IAuthenticationService
{
    public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
    {
        string sessionIdString;
        try
        {
            sessionIdString = await decryptor.Decrypt(authToken, cancellationToken);
        }

        catch (CryptographicException ex)
        {
            logger.LogWarning(ex,"Authentication failed: invalid token");
            return User.Guest;
        }

        if (!Guid.TryParse(sessionIdString, out var sessionId))
        {
            logger.LogWarning("Authentication failed: invalid token");
            return User.Guest;
        }

        var session = await storage.FindSession(sessionId, cancellationToken);
        
        if (session is null || session.ExpiresAt <= DateTimeOffset.UtcNow)
            return User.Guest;

        return new User(session.UserId, sessionId);
    }
}   
