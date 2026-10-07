using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.Intentions;
using FluentValidation;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

internal class CreateConversationUseCase(
    IValidator<CreateConversationCommand> validator,
    IIntentionManager intentionManager,
    ICreateConversationStorage storage,
    IIdentityProvider provider)
    : ICreateConversationUseCase
{
    public async Task<Conversation> Execute(CreateConversationCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        intentionManager.ThrowIfForbidden(ConversationIntentions.Create);
        return await storage.CreateConversationAsync(provider.Current.UserId, command.Name, cancellationToken);
    }
}
