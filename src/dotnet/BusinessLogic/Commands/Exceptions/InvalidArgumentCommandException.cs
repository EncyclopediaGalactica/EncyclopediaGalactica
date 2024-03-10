namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

public class InvalidArgumentCommandException : Exception
{
    public InvalidArgumentCommandException()
    {
    }

    protected InvalidArgumentCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidArgumentCommandException(string? message) : base(message)
    {
    }

    public InvalidArgumentCommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}