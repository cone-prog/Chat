using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.UseCase.Conversations.Intentions;
using MediatR;

namespace Chat.Domain.UseCase.Conversations.GetConversationById;

internal class GetConversationByIdUseCase(
    IGetConversationByIdStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager)
    : IRequestHandler<GetConversationByIdQuery, Conversation>
{
    public async Task<Conversation> Handle(GetConversationByIdQuery query, CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(ConversationIntentions.GetById);
        var conversation = await storage.GetByIdAsync(query.Id, cancellationToken);
        return conversation ?? throw new ConversationNotFoundException(query.Id);
    }
}
