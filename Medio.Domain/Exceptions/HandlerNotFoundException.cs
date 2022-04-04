namespace Medio.Domain.Exceptions;

public class HandlerNotFoundException : Exception
{
    public HandlerNotFoundException(object sender, string? message) 
        : base(sender.GetType().FullName + " : " + message)
    {
    }
}
