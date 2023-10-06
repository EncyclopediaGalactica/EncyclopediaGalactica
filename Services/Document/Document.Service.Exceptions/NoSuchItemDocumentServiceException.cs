namespace EncyclopediaGalactica.Services.Document.Service.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     No Such Item Document Service Exception
///     <remarks>
///         It is thrown when the no item has been found based on entity id.
///     </remarks>
/// </summary>
public class NoSuchItemDocumentServiceException : Exception
{
    public NoSuchItemDocumentServiceException()
    {
    }

    protected NoSuchItemDocumentServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoSuchItemDocumentServiceException(string? message) : base(message)
    {
    }

    public NoSuchItemDocumentServiceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}