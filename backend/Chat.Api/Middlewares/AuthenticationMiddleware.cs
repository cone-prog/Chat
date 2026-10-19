using Chat.Api.Authentication;
using Chat.Domain.Authentication;

namespace Chat.Api.Middlewares;

public class AuthenticationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext httpContext,
        IAuthenticationService authenticationService,
        IAuthTokenStorage authTokenStorage,
        IIdentityProvider identityProvider)
    {
        if (authTokenStorage.TryExtract(httpContext, out string token))
        {
            var identity = await authenticationService.Authenticate(token, httpContext.RequestAborted);
            identityProvider.Current = identity;
        }
        else identityProvider.Current = User.Guest;
        await next.Invoke(httpContext);
    }
}