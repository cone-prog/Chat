using Castle.Components.DictionaryAdapter.Xml;
using Chat.Domain.Authentication;
using Chat.Domain.UseCase.Users.SignOn;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;

namespace Chat.Domain.Tests.Users;

public class SignOnUseCaseShould
{
    private readonly ISetup<ISignOnStorage, Task<Guid>> CreateUserSetup;
    private readonly ISetup<IPasswordManager, (byte[] Salt, byte[] hash)> generatePasswordParts;
    private readonly SignOnUseCase sut;
    private readonly Mock<ISignOnStorage> storage;

    public SignOnUseCaseShould()
    {
        var validator = new Mock<IValidator<SignOnCommand>>();
        validator.Setup(v => v.ValidateAsync(It.IsAny<SignOnCommand>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        storage = new Mock<ISignOnStorage>();
        CreateUserSetup = storage.Setup(s => s.Create(It.IsAny<string>(),It.IsAny<string>(), 
            It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()));

        var passwordManager = new Mock<IPasswordManager>();
        generatePasswordParts = passwordManager.Setup(p => p.GeneratePasswordParts(It.IsAny<string>()));
        
        sut = new SignOnUseCase(validator.Object, storage.Object, passwordManager.Object);
    }

    [Fact]
    public async Task CreateUserWithGeneratePasswordParts()
    {
        var salt = new byte[] {1};
        var passwordHash = new byte[] {2};
        var command = new SignOnCommand("somebody","sadfa", "asdf");
        generatePasswordParts.Returns((salt, passwordHash));
        
        await sut.Execute(command, CancellationToken.None);

        storage.Verify(s => s.Create(command.Name,command.Email, passwordHash, salt, It.IsAny<CancellationToken>()),
            Times.Once);
    }
    
    [Fact]
    public async Task ReturnIdentityOfNewlyCreatedUser()
    {
        var salt = new byte[] {1};
        var passwordHash = new byte[] {2};
        generatePasswordParts.Returns((salt, passwordHash));

        var userId = Guid.Parse("63A0B10D-E1E3-41D8-A303-A9DAC20EEE92");
        CreateUserSetup.ReturnsAsync(userId);
        
        
        var command = new SignOnCommand("somebody","sadfa", "asdf");
        var result = await sut.Execute(command, CancellationToken.None);

        Assert.Equal(userId, result.UserId);
    }
}