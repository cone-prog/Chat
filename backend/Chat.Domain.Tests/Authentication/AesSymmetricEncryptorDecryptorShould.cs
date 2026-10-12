using System.Security.Cryptography;
using Chat.Domain.Authentication;
using Xunit;

namespace Chat.Domain.Tests.Authentication;

public class AesSymmetricEncryptorDecryptorShould(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
{
    private readonly AesSymmetricEncryptorDecryptor sut = new AesSymmetricEncryptorDecryptor();

    [Fact]
    public async Task DecryptEncryptedString_WhenKeyIsSame()
    {
        var text = "Hello";
        var key = RandomNumberGenerator.GetBytes(32);
        var encryptedText = await sut.Encrypt(text, key, CancellationToken.None);
        var decryptedText = await sut.Decrypt(encryptedText, key, CancellationToken.None);
        testOutputHelper.WriteLine(decryptedText);
        Assert.Equal(text, decryptedText);
    }

    [Fact]
    public async Task ThrowCryptographicException_WhenKeyIsDifferent()
    {
        var text = "Hello";
        var encryptedText = await sut.Encrypt(text, RandomNumberGenerator.GetBytes(32), CancellationToken.None);

        await Assert.ThrowsAsync<CryptographicException>(() => sut.Decrypt(encryptedText,
            RandomNumberGenerator.GetBytes(32), CancellationToken.None));
    }

}
