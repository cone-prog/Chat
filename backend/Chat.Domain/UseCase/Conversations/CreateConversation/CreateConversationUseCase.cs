using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.Intentions;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

public class CreateConversationUseCase(
    IIntentionManager intentionManager,
    ICreateConversationStorage storage,
    IIdentityProvider provider)
    : ICreateConversationUseCase
{
    public async Task<Conversation> Execute(string name, CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(ConversationIntentions.Create);
        return await storage.CreateConversationAsync(provider.Current.UserId, name, cancellationToken);
    }
}
