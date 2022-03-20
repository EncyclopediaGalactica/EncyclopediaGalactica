namespace EncyclopediaGalactica.SourceFormats.Mappers.Exceptions.SourceFormatNode;

using System.Runtime.Serialization;

public class SourceFormatNodeMapperException : Exception
{
    public SourceFormatNodeMapperException()
    {
    }

    protected SourceFormatNodeMapperException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public SourceFormatNodeMapperException(string? message) : base(message)
    {
    }

    public SourceFormatNodeMapperException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}