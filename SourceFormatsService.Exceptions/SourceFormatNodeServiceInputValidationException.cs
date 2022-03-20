namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Exceptions;

using System.Runtime.Serialization;

public class SourceFormatNodeServiceInputValidationException : Exception
{
    public SourceFormatNodeServiceInputValidationException()
    {
    }

    protected SourceFormatNodeServiceInputValidationException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public SourceFormatNodeServiceInputValidationException(string? message) : base(message)
    {
    }

    public SourceFormatNodeServiceInputValidationException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}