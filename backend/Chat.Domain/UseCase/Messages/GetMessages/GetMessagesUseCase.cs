using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;

namespace Chat.Domain.UseCase.Messages.GetMessages;

internal class GetMessagesUseCase(
    IValidator<GetMessagesQuery> validator,
    IGetMessagesStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager)
: IGetMessagesUseCase
{
    public async Task<IEnumerable<Message>> Execute(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        intentionManager.ThrowIfForbidden(MessageIntentions.Get);
        return await storage.GetMessagesAsync(query.ConversationId, query.BeforeMessageId, identityProvider.Current.UserId,
            query.Limit, cancellationToken);
    }
}
