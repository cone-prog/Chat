namespace Chat.Domain.UseCase.Messages.CreateMessage;

public record CreateMessageCommand(Guid ConversationId, string Text);
