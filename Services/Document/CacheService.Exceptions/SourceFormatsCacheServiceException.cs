namespace EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.Exceptions;

using System.Runtime.Serialization;

public class SourceFormatsCacheServiceException : Exception
{
    public SourceFormatsCacheServiceException()
    {
    }

    protected SourceFormatsCacheServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public SourceFormatsCacheServiceException(string? message) : base(message)
    {
    }

    public SourceFormatsCacheServiceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}