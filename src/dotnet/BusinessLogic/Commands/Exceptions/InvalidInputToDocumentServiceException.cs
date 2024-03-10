namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     Invalid Input To Document Service Exception
///     <remarks>
///         It is thrown when the provided input fails at the inner validation of the service.
///     </remarks>
/// </summary>
public class InvalidInputToDocumentServiceException : Exception
{
    public InvalidInputToDocumentServiceException()
    {
    }

    protected InvalidInputToDocumentServiceException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public InvalidInputToDocumentServiceException(string? message) : base(message)
    {
    }

    public InvalidInputToDocumentServiceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}