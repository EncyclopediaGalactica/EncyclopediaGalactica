namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     Document Service Operation Cancelled Exception
///     <remarks>
///         When a Document Service operation is cancelled by providing a <see cref="CancellationToken" />.
///     </remarks>
/// </summary>
public class DocumentServiceOperationCancelledException : Exception
{
    public DocumentServiceOperationCancelledException()
    {
    }

    protected DocumentServiceOperationCancelledException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public DocumentServiceOperationCancelledException(string? message) : base(message)
    {
    }

    public DocumentServiceOperationCancelledException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}