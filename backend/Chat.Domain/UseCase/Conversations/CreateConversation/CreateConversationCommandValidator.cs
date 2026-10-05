using FluentValidation;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

public class CreateConversationCommandValidator
    : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithErrorCode("Empty")
            .MaximumLength(70).WithErrorCode("TooLong");
    }
}
