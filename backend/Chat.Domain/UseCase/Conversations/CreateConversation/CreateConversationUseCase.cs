using Chat.Domain.Authentication;
using Chat.Domain.Entities;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

public class CreateConversationUseCase(ICreateConversationStorage storage, IIdentityProvider provider)
    : ICreateConversationUseCase
{
    public async Task<Conversation> Execute(string name, CancellationToken cancellationToken)
    {
        return await storage.CreateConversationAsync(provider.Current.UserId, name, cancellationToken);
    }
}
