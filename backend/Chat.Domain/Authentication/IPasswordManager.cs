namespace Chat.Domain.Authentication;

internal interface IPasswordManager
{
    (byte[] Salt, byte[] hash) GeneratePasswordParts(string password);

    bool ComparePassword(string password, byte[] salt, byte[] hash);
}
