using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;

namespace Chat.Domain.UseCase.Messages.CreateMessage;

internal class CreateMessageUseCase(
    ICreateMessageStorage storage,
    IValidator<CreateMessageCommand> validator,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager)
    : ICreateMessageUseCase
{
    public async Task<Message> Execute(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        if (!await storage.ConversationExistsAsync(command.ConversationId, cancellationToken))
            throw new ConversationNotFoundException(command.ConversationId);

        intentionManager.ThrowIfForbidden(MessageIntentions.Create);

        return await storage.CreateAsync(command.ConversationId, identityProvider.Current.UserId,
            command.Text, cancellationToken);
    }
}
