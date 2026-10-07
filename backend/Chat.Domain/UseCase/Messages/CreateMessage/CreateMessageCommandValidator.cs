using Chat.Domain.Exceptions;
using FluentValidation;

namespace Chat.Domain.UseCase.Messages.CreateMessage;

internal class CreateMessageCommandValidator
    : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(c => c.ConversationId)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);

        RuleFor(c => c.Text)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
    }
}
