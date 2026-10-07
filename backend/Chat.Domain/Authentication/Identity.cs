namespace Chat.Domain.Authentication;

internal class User : IIdentity
{
    public User(Guid id)
    {
        UserId = id;
    }

    public Guid UserId { get; }
}
