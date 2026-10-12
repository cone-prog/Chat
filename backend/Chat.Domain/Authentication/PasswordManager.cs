using System.Security.Cryptography;

namespace Chat.Domain.Authentication;

public class PasswordManager : IPasswordManager
{
    private const int SaltLength = 16;

    public (byte[] salt, byte[] hash) GeneratePasswordParts(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltLength);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 600000, HashAlgorithmName.SHA256, 32);
        return (salt, hash);
    }

    public bool ComparePassword(string password, byte[] salt, byte[] hash)
    {
        var computeHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 600000, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(hash, computeHash);
    }
}
