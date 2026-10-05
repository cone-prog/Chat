namespace Chat.Domain.Exceptions;

public class ConversationNotFoundExcetption : DomainException
{
    public ConversationNotFoundExcetption(Guid id) : base(ErrorCode.Gone, $"Conversation with id {id} not found")
    {

    }
}
