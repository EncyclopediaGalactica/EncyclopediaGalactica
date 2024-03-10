namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     Document Service Operation Cancelled Exception
///     <remarks>
///         When a Document Service operation is cancelled by providing a <see cref="CancellationToken" />.
///     </remarks>
/// </summary>
public class CommandCancelledException : Exception
{
    public CommandCancelledException()
    {
    }

    protected CommandCancelledException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public CommandCancelledException(string? message) : base(message)
    {
    }

    public CommandCancelledException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}