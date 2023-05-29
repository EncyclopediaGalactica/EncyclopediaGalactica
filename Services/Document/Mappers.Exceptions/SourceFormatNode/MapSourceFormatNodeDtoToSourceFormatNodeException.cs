namespace EncyclopediaGalactica.Services.Document.Mappers.Exceptions.SourceFormatNode;

using System.Runtime.Serialization;

public class MapSourceFormatNodeDtoToSourceFormatNodeException : Exception
{
    public MapSourceFormatNodeDtoToSourceFormatNodeException()
    {
    }

    protected MapSourceFormatNodeDtoToSourceFormatNodeException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public MapSourceFormatNodeDtoToSourceFormatNodeException(string? message) : base(message)
    {
    }

    public MapSourceFormatNodeDtoToSourceFormatNodeException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}