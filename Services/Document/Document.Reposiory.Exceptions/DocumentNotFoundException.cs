namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Exceptions;

using System.Runtime.Serialization;

public class DocumentNotFoundException : Exception
{
    public DocumentNotFoundException()
    {
    }

    protected DocumentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DocumentNotFoundException(string? message) : base(message)
    {
    }

    public DocumentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}