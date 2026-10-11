using Chat.Domain.Authentication;
using Chat.Domain.Authorization;

namespace Chat.Domain.UseCase.Messages.Intentions;

internal class MessageIntentionResolver
    : IIntentionResolver<MessageIntentions>
{
    public bool IsAllowed(IIdentity subject, MessageIntentions intention)
    {
        switch (intention)
        {
            case MessageIntentions.Create:
                return true;
            case MessageIntentions.Get:
                return true;
            default:
                return false;
        }
    }
}
