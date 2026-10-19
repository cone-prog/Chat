using Chat.Domain.Authentication;
using Chat.Domain.Exceptions;
using Chat.Domain.UseCase.Users.SignIn;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;

namespace Chat.Domain.Tests.Users;

public class SignInUseCaseShould
{
    private readonly ISetup<ISymmetricEncryptor, Task<string>> encryptSetup;
    private readonly ISetup<ISignInStorage, Task<RecognisedUser?>> findUserSetup;
    private readonly Mock<ISignInStorage> storage;
    private readonly SignInUseCase sut;
    private readonly ISetup<IPasswordManager, bool> comparePasswordSetup;
    private readonly ISetup<ISignInStorage, Task<Guid>> CreateSessionSetup;

    public SignInUseCaseShould()
    {
        var validator = new Mock<IValidator<SignInCommand>>();
        validator
            .Setup(v => v.ValidateAsync(It.IsAny<SignInCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var encryptor = new Mock<ISymmetricEncryptor>();
        encryptSetup = encryptor.Setup(e => e.Encrypt(It.IsAny<string>(),
            It.IsAny<CancellationToken>()));
        
        storage = new Mock<ISignInStorage>();
        findUserSetup = storage.Setup(s => s.FindUser(It.IsAny<string>(), It.IsAny<CancellationToken>()));
        CreateSessionSetup = storage.Setup(s => s.CreateSession(It.IsAny<Guid>(), It.IsAny<DateTimeOffset>(),
            It.IsAny<CancellationToken>()));
        
        var passwordManager = new Mock<IPasswordManager>();
        comparePasswordSetup = passwordManager.Setup(p => p.ComparePassword(It.IsAny<string>(),It.IsAny<byte[]>(),It.IsAny<byte[]>()));
        
        sut = new SignInUseCase(encryptor.Object, storage.Object, passwordManager.Object);
    }

    [Fact]
    public async Task ReturnTokenAndIdentity()
    {
        var userId = Guid.Parse("1982400E-6D68-41AE-BD0D-76BB2C99B1DD");
        var sessionId = Guid.Parse("2982400E-6D68-41AE-BD0D-76BB2C99B1DD");
        var expected = new User(userId, sessionId);
        findUserSetup.ReturnsAsync(new RecognisedUser
        {
            Id = userId,
            PasswordHash = new byte[] { 1 }, Salt = new byte[] { 3 }
        });
        var expectedToken = "token";
        encryptSetup.ReturnsAsync(expectedToken);
        CreateSessionSetup.ReturnsAsync(sessionId);
        comparePasswordSetup.Returns(true);

        var (actualUser, actualToken) = await sut.Handle(new SignInCommand("somebody", "gg"), CancellationToken.None);
        Assert.NotNull(actualUser);
        Assert.Equal(expected.UserId,actualUser.UserId);
        Assert.Equal(expected.SessionId, actualUser.SessionId);
        Assert.Equal(expectedToken, actualToken);
    }

    [Fact]
    public async Task ThrowUserNotFoundException_WhenUserNotFound()
    {
        findUserSetup.ReturnsAsync((RecognisedUser?)null);
        
        await Assert.ThrowsAsync<UserNotFoundEcxeption>(() => sut.Handle(new SignInCommand("somebody", "gg"),
            CancellationToken.None));
    }
    

    [Fact]
    public async Task ThrowUserNotFoundException_WhenPasswordDoesntMatch()
    {
        findUserSetup.ReturnsAsync(new RecognisedUser { Salt = [], PasswordHash = [] });
        comparePasswordSetup.Returns(false);

        await Assert.ThrowsAsync<UserNotFoundEcxeption>(() => sut.Handle(new SignInCommand("somebody", "gg"), 
            CancellationToken.None));
    }
}