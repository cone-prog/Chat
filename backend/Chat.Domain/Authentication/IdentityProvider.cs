namespace Chat.Domain.Authentication;

internal class IdentityProvider : IIdentityProvider
{
    public IIdentity Current => new User(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"));
}
