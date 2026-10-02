namespace Chat.Domain.Authentication;

public interface IIdentity
{
    Guid UserId { get; }
}
