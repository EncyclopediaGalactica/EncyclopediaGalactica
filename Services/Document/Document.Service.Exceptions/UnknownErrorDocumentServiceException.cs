namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     Unknown Error At Document Service Exception
///     <remarks>
///         It is thrown when an unknown error happens in the service.
///     </remarks>
/// </summary>
public class UnknownErrorDocumentServiceException : Exception
{
    public UnknownErrorDocumentServiceException()
    {
    }

    protected UnknownErrorDocumentServiceException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public UnknownErrorDocumentServiceException(string? message) : base(message)
    {
    }

    public UnknownErrorDocumentServiceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}