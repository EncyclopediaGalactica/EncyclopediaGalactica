namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     Document Service Operation Cancelled Exception
///     <remarks>
///         When a Document Service operation is cancelled by providing a <see cref="CancellationToken" />.
///     </remarks>
/// </summary>
public class OperationCancelledCommandException : Exception
{
    public OperationCancelledCommandException()
    {
    }

    protected OperationCancelledCommandException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public OperationCancelledCommandException(string? message) : base(message)
    {
    }

    public OperationCancelledCommandException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}