namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     No Such Item Document Service Exception
///     <remarks>
///         It is thrown when the no item has been found based on entity id.
///     </remarks>
/// </summary>
public class NoSuchItemCommandException : Exception
{
    public NoSuchItemCommandException()
    {
    }

    protected NoSuchItemCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoSuchItemCommandException(string? message) : base(message)
    {
    }

    public NoSuchItemCommandException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}