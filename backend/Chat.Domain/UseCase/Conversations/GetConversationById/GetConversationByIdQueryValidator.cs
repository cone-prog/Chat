using Chat.Domain.Exceptions;
using FluentValidation;

namespace Chat.Domain.UseCase.Conversations.GetConversationById;

public class GetConversationByIdQueryValidator : AbstractValidator<GetConversationByIdQuery>
{
    public GetConversationByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
    }
}