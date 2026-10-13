namespace Chat.Domain.Authentication;

public interface ISymmetricDecryptor
{
    Task<string> Decrypt(string encryptedText, CancellationToken cancellationToken);
}
