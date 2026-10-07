using Chat.Domain.UseCase.Messages.CreateMessage;
using FluentValidation.TestHelper;
using Xunit;

namespace Chat.Domain.Tests.Messages;

public class CreateMessageCommandValidatorShould
{
    private readonly CreateMessageCommandValidator sut = new();

    [Fact]
    public async Task ReturnAccess_WhenCommandIsValid()
    {
        var actual = new CreateMessageCommand(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), "Something");
        sut.TestValidate(actual).ShouldNotHaveAnyValidationErrors();
    }

    public static IEnumerable<object[]> GetInvalidCommands()
    {
        var validCommands = new CreateMessageCommand(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), "Something");
        yield return new object[] { validCommands with { ConversationId = Guid.Empty } };
        yield return new object[] { validCommands with { Text = "" } };
        yield return new object[] { validCommands with { Text = "          " } };
    }

    [Theory]
    [MemberData(nameof(GetInvalidCommands))]
    public async Task ReturnFailure_WhenCommandIsInvalid(CreateMessageCommand command)
    {
        var actual = command;
        sut.TestValidate(command).ShouldHaveValidationErrors();
    }
}

