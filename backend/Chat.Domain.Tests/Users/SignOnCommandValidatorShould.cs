using Chat.Domain.UseCase.Users.SignOn;
using FluentValidation.TestHelper;

namespace Chat.Domain.Tests.Users;

public class SignOnCommandValidatorShould
{
    private readonly SignOnCommandValidator sut = new();
    [Fact]
    public void ReturnSuccess_WhenCommandIsValid()
    {
        var command = new SignOnCommand("mmm" ,"asdfasdf@ff.ee", "asdg");
        sut.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("mmmmmmmm","", "dsfsd")]
    [InlineData("ffffffff", "sadf", "")]
    [InlineData("","sadf", "afsdfsad")]
    [InlineData("ggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg","sadf", "")]
    public void ReturnFailure_WhenCommandIsInvalid(string name, string email, string password)
    {
        var command = new SignOnCommand(name, email, password);
        sut.TestValidate(command).ShouldHaveValidationErrors();
    }
}