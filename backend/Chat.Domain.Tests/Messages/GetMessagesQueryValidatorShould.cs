using Chat.Domain.UseCase.Messages.GetMessages;
using FluentValidation.TestHelper;
using Xunit;

namespace Chat.Domain.Tests.Messages;

public class GetMessagesQueryValidatorShould
{
    private readonly GetMessagesQueryValidator sut = new();

    public async Task ReturnsSuccess_WhenQueryIsValid()
    {
        var query = new GetMessagesQuery(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), 20);

        var actual = await sut.TestValidateAsync(query);
        actual.ShouldNotHaveAnyValidationErrors();
    }

    public static IEnumerable<object[]> GetInvalidQueries()
    {
        var validQuery = new GetMessagesQuery(Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), 20);
        yield return new object[] { validQuery with { ConversationId = Guid.Empty } };
        yield return new object[] { validQuery with { Limit = -1 } };
    }

    [Theory]
    [MemberData(nameof(GetInvalidQueries))]
    public async Task ReturnFailure_WhenQueryInvalid(GetMessagesQuery query)
    {
        var actual = await sut.TestValidateAsync(query);
        actual.ShouldHaveValidationErrors();
    }
}
