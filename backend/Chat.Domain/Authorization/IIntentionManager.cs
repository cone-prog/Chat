using Chat.Domain.Authentication;

namespace Chat.Domain.Authorization;

public interface IIntentionManager
{
    bool IsAllowed<TIntention>(TIntention intention) where TIntention : struct;

    bool IsAllowed<TIntention, TObject>(TIntention intention, TObject obj) where TIntention : struct;
}

public class IntentionManager(IEnumerable<IIntentionResolver> intentionResolvers, IIdentityProvider identityProvider)
    : IIntentionManager
{
    public bool IsAllowed<TIntention>(TIntention intention) where TIntention : struct
    {
        var matchingResolver = intentionResolvers.OfType<IIntentionResolver<TIntention>>().FirstOrDefault();
        return matchingResolver?.IsAllowed(identityProvider.Current, intention) ?? false;
    }

    public bool IsAllowed<TIntention, TObject>(TIntention intention, TObject obj) where TIntention : struct
    {
        throw new NotImplementedException();
    }
}

public static class IntentionManagerExtentions
{
    public static void ThrowIfForbidden<TIntention>(this IIntentionManager intentionManager, TIntention intention)
        where TIntention : struct
    {
        if (!intentionManager.IsAllowed(intention)) throw new IntentionManagerException();
    }
}