namespace Chat.Domain.Authentication;

public interface ISymmetricEncryptor
{
    Task<string> Encrypt(string plainText, CancellationToken cancellationToken);
}
