using Chat.Domain.Authentication;
using Chat.Domain.Authorization;

namespace Chat.Domain.UseCase.Conversations.Intentions;

internal class ConversationIntentionResolver : IIntentionResolver<ConversationIntentions>
{
    public bool IsAllowed(IIdentity subject, ConversationIntentions intention)
    {
        switch (intention)
        {
            case ConversationIntentions.GetById:
                return true;
            case ConversationIntentions.Create:
                return subject.IsAuthenticated();
            default:
                return false;
        }
    }
}
