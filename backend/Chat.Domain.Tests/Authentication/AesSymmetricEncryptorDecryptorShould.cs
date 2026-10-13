using System.Security.Cryptography;
using Chat.Domain.Authentication;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Language.Flow;
using Xunit;

namespace Chat.Domain.Tests.Authentication;

public class AesSymmetricEncryptorDecryptorShould
{
    private readonly AesSymmetricEncryptorDecryptor sut;
    private readonly ISetup<IOptions<AuthenticationConfiguration>, AuthenticationConfiguration> keyValueSetup;

    public AesSymmetricEncryptorDecryptorShould()
    {
        var option = new Mock<IOptions<AuthenticationConfiguration>>();
        keyValueSetup = option.Setup(o => o.Value);
        sut =  new AesSymmetricEncryptorDecryptor(option.Object);
    }

    [Fact]
    public async Task DecryptEncryptedString_WhenKeyIsSame()
    {
        var text = "Hello";
        keyValueSetup.Returns(new AuthenticationConfiguration
        {
            Base64Key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
        });
        
        var encryptedText = await sut.Encrypt(text,  CancellationToken.None);
        var decryptedText = await sut.Decrypt(encryptedText,  CancellationToken.None);
        Assert.Equal(text, decryptedText);
    }

    [Fact]
    public async Task ThrowCryptographicException_WhenKeyIsDifferent()
    {
        var text = "Hello";
        keyValueSetup.Returns(new AuthenticationConfiguration
        {
            Base64Key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
        });
        var encryptedText = await sut.Encrypt(text,CancellationToken.None);
        keyValueSetup.Returns(new AuthenticationConfiguration
        {
            Base64Key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
        });
        await Assert.ThrowsAsync<CryptographicException>(() => sut.Decrypt(encryptedText, CancellationToken.None));
    }

}
