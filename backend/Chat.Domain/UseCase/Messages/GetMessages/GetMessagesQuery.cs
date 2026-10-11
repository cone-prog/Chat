namespace Chat.Domain.UseCase.Messages.GetMessages;

public record GetMessagesQuery(Guid ConversationId, Guid? BeforeMessageId, int Limit);
