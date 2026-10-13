using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace Chat.Domain.Authentication;

internal class AesSymmetricEncryptorDecryptor(IOptions<AuthenticationConfiguration> option) : ISymmetricEncryptor, ISymmetricDecryptor
{
    private const int IvSize = 16;
    private readonly Lazy<Aes> aes = new(Aes.Create);

    public async Task<string> Encrypt(string plainText,  CancellationToken cancellationToken)
    {
        var iv = RandomNumberGenerator.GetBytes(IvSize);

        using var encryptedStream = new MemoryStream();
        await encryptedStream.WriteAsync(iv, cancellationToken);
        var encryptor = aes.Value.CreateEncryptor(option.Value.Key, iv);

        await using (var stream = new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write))
        {
            await stream.WriteAsync(Encoding.UTF8.GetBytes(plainText), cancellationToken);
        }

        return Convert.ToBase64String(encryptedStream.ToArray());
    }

    public async Task<string> Decrypt(string encryptedText,  CancellationToken cancellationToken)
    {
        var encryptedBytes = Convert.FromBase64String(encryptedText);

        var iv = new byte[IvSize];
        Array.Copy(encryptedBytes, iv, IvSize);
        using var decryptedStream = new MemoryStream();

        var decryptor = aes.Value.CreateDecryptor(option.Value.Key, iv);
        await using (var stream = new CryptoStream(decryptedStream, decryptor, CryptoStreamMode.Write))
        {
            await stream.WriteAsync(encryptedBytes.AsMemory(IvSize), cancellationToken);
        }

        return Encoding.UTF8.GetString(decryptedStream.ToArray());
    }
}
