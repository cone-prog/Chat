namespace Chat.Api.Authentication;

public class AuthTokenStorage : IAuthTokenStorage
{
    public bool TryExtract(HttpContext httpContext, out string token)
    {
        if (httpContext.Request.Headers.TryGetValue("token", out var authToken) &&
            !string.IsNullOrWhiteSpace(authToken))
        {
            token = authToken;
            return true;
        }

        token =  string.Empty;
        return false;
    }

    public void Store(HttpContext httpContext, string token)
    {
        httpContext.Response.Headers["token"] =  token;
    }
}