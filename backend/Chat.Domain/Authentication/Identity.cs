namespace Chat.Domain.Authentication;

public class User : IIdentity
{
    public User(Guid id, Guid sessionId)
    {
        UserId = id;
        SessionId = sessionId;
    }

    public Guid UserId { get; }
    public Guid SessionId { get; }
    
    public static User Guest => new(Guid.Empty, Guid.Empty);

}
