namespace Chat.Domain.Exceptions;

public class UserNotFoundEcxeption : DomainException
{
    public UserNotFoundEcxeption(string email) 
        : base(DomainErrorCode.NotFound ,$"User with email '{email}' could not be found'")
    {
        
    }
}