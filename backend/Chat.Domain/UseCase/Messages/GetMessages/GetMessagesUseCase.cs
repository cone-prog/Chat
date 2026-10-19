using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;
using MediatR;

namespace Chat.Domain.UseCase.Messages.GetMessages;

internal class GetMessagesUseCase(
    IGetMessagesStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager)
: IRequestHandler<GetMessagesQuery, IEnumerable<Message>>
{
    public async Task<IEnumerable<Message>> Handle(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(MessageIntentions.Get);
        return await storage.GetMessagesAsync(query.ConversationId, query.BeforeMessageId, identityProvider.Current.UserId,
            query.Limit, cancellationToken);
    }
}
