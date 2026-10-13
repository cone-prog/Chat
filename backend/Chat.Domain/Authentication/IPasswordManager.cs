namespace Chat.Domain.Authentication;

public interface IPasswordManager
{
    (byte[] salt, byte[] hash) GeneratePasswordParts(string password);

    bool ComparePassword(string password, byte[] salt, byte[] hash);
}
