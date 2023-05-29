namespace EncyclopediaGalactica.Client.Core.Exceptions;

using System.Runtime.Serialization;

public class SourceFormatsSdkCoreException : Exception
{
    public SourceFormatsSdkCoreException()
    {
    }

    protected SourceFormatsSdkCoreException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public SourceFormatsSdkCoreException(string? message) : base(message)
    {
    }

    public SourceFormatsSdkCoreException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}