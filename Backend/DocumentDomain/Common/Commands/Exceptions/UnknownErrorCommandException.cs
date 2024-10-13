namespace EncyclopediaGalactica.DocumentDomain.Common.Commands.Exceptions;

using System.Runtime.Serialization;

public class UnknownErrorCommandException : Exception
{
    public UnknownErrorCommandException()
    {
    }

    protected UnknownErrorCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UnknownErrorCommandException(string? message) : base(message)
    {
    }

    public UnknownErrorCommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}