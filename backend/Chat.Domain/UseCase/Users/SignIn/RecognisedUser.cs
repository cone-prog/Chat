namespace Chat.Domain.UseCase.Users.SignIn;

public class RecognisedUser
{
    public Guid Id { get; set; }

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;
}