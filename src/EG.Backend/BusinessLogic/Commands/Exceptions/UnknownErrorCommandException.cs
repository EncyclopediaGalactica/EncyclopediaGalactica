namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     Unknown Error At Document Service Exception
///     <remarks>
///         It is thrown when an unknown error happens in the service.
///     </remarks>
/// </summary>
public class UnknownErrorCommandException : Exception
{
    public UnknownErrorCommandException()
    {
    }

    protected UnknownErrorCommandException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public UnknownErrorCommandException(string? message) : base(message)
    {
    }

    public UnknownErrorCommandException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}