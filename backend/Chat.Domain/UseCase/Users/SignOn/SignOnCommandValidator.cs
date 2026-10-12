using Chat.Domain.Exceptions;
using FluentValidation;

namespace Chat.Domain.UseCase.Users.SignOn;

public class SignOnCommandValidator : AbstractValidator<SignOnCommand>
{
    public SignOnCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);

        RuleFor(command => command.Password)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
        
        RuleFor(command => command.Name)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty)
            .MaximumLength(30).WithErrorCode(ValidationErrorCode.TooLong);
    }
}