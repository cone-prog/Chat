namespace Chat.Domain.Authentication;

public class User : IIdentity
{
    public User(Guid id)
    {
        UserId = id;
    }

    public Guid UserId { get; }
}
