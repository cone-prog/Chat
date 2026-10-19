using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.Monitoring;
using Chat.Domain.UseCase.Conversations.Intentions;
using MediatR;

namespace Chat.Domain.UseCase.Conversations.GetConversations;

public class GetConversationsUseCase(
    IGetConversationsStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager
) : IRequestHandler<GetConversationsQuery, List<Conversation>>
{
    public Task<List<Conversation>> Handle(GetConversationsQuery query,CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(ConversationIntentions.Get);
        return storage.GetConversations(identityProvider.Current.UserId, cancellationToken);
    }
    
}
