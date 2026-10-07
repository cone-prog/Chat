using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.UseCase.Conversations.Intentions;

namespace Chat.Domain.UseCase.Conversations.GetConversationById;

internal class GetConversationByIdUseCase(
    IGetConversationByIdStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager)
    : IGetConversationByIdUseCase
{
    public async Task<Conversation> Execute(Guid id, CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(ConversationIntentions.GetById);
        var conversation = await storage.GetByIdAsync(id, cancellationToken);
        if (conversation is null) throw new ConversationNotFoundException(id);
        return conversation;
    }
}
