using Chat.Domain.Exceptions;
using FluentValidation;

namespace Chat.Domain.UseCase.Users.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
        
        RuleFor(c => c.Password)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
    }
}