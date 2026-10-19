using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;
using MediatR;

namespace Chat.Domain.UseCase.Messages.CreateMessage;

internal class CreateMessageUseCase(
    ICreateMessageStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager)
    : IRequestHandler<CreateMessageCommand, Message>
{
    public async Task<Message> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        if (!await storage.ConversationExistsAsync(command.ConversationId, cancellationToken))
            throw new ConversationNotFoundException(command.ConversationId);

        intentionManager.ThrowIfForbidden(MessageIntentions.Create);

        return await storage.CreateAsync(command.ConversationId, identityProvider.Current.UserId,
            command.Text, cancellationToken);
    }
}
