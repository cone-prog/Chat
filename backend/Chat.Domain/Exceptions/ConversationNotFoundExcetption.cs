namespace Chat.Domain.Exceptions;

public class ConversationNotFoundExcetption : Exception
{
    public ConversationNotFoundExcetption(Guid id) : base($"Conversation with id {id} not found")
    {

    }
}
