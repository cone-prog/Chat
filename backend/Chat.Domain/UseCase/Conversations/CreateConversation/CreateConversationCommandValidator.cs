using Chat.Domain.Exceptions;
using FluentValidation;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

internal class CreateConversationCommandValidator
    : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty)
            .MaximumLength(70).WithErrorCode(ValidationErrorCode.TooLong);
    }
}
