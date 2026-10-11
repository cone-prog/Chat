using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.Intentions;

namespace Chat.Domain.UseCase.Conversations.GetConversations;

public class GetConversationsUseCase(
    IGetConversationsStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager
) : IGetConversationsUseCase
{
    public Task<List<Conversation>> Execute(CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(ConversationIntentions.Get);
        return storage.GetConversations(identityProvider.Current.UserId, cancellationToken);
    }
}
