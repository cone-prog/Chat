using Chat.Domain.Authentication;

namespace Chat.Domain.Authorization;

public interface IIntentionResolver { }

public interface IIntentionResolver<TIntention> : IIntentionResolver
{
    bool IsAllowed(IIdentity subject, TIntention intention);
}
