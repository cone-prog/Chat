using Chat.Domain.Exceptions;
using FluentValidation;

namespace Chat.Domain.UseCase.Messages.GetMessages;

internal class GetMessagesQueryValidator
    : AbstractValidator<GetMessagesQuery>
{
    public GetMessagesQueryValidator()
    {
        RuleFor(q => q.ConversationId)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);

        RuleFor(q => q.Limit)
            .GreaterThanOrEqualTo(0).WithErrorCode(ValidationErrorCode.Invalid);
    }
}
