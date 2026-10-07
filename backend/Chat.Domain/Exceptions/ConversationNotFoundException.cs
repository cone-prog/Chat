namespace Chat.Domain.Exceptions;

public class ConversationNotFoundException : DomainException
{
    public ConversationNotFoundException(Guid id) : base(DomainErrorCode.Gone, $"Conversation with id {id} not found")
    {

    }
}
