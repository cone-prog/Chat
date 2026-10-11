using Chat.Domain.UseCase.Conversations.CreateConversation;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Chat.Domain.Tests.Conversations;

public class CreateConversationCommandValidatorShould
{
    private readonly IValidator<CreateConversationCommand> sut;
    public CreateConversationCommandValidatorShould()
    {
        sut = new CreateConversationCommandValidator();
    }

    [Fact]
    public void ReturnSuccess_WhenCommandIsValid()
    {
        var actual = sut.TestValidate(new CreateConversationCommand("Гэнста рэп"));
        actual.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("        "),]
    [InlineData("")]
    [InlineData("dcysxbrgfndyeeabpikrwnsjcjznkdlhcacocykknctpeksneajsmiletlwkbaahqqrxrkszfsnuioex")]
    public void ReturnFailure_WhenCommandInvalid(string name)
    {
        var command = new CreateConversationCommand(name);
        var actual = sut.TestValidate(command);
        actual.ShouldHaveValidationErrors();
    }
}
