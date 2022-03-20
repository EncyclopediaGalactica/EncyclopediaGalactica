namespace EncyclopediaGalactica.SourceFormats.Mappers.Exceptions.SourceFormatNode;

using System.Runtime.Serialization;

public class MapSourceFormatNodeToSourceFormatNodeDtoException : Exception
{
    public MapSourceFormatNodeToSourceFormatNodeDtoException()
    {
    }

    protected MapSourceFormatNodeToSourceFormatNodeDtoException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public MapSourceFormatNodeToSourceFormatNodeDtoException(string? message) : base(message)
    {
    }

    public MapSourceFormatNodeToSourceFormatNodeDtoException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}