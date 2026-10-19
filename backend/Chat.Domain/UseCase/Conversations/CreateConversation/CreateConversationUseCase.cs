using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.Intentions;
using FluentValidation;
using MediatR;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

internal class CreateConversationUseCase(
    IIntentionManager intentionManager,
    ICreateConversationStorage storage,
    IIdentityProvider provider)
    : IRequestHandler<CreateConversationCommand, Conversation>
{
    public async Task<Conversation> Handle(CreateConversationCommand command, CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(ConversationIntentions.Create);
        return await storage.CreateConversationAsync(provider.Current.UserId, command.Name, cancellationToken);
    }
}
