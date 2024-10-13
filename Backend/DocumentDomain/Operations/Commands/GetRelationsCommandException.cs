namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using System.Runtime.Serialization;

public class GetRelationsCommandException : Exception
{
    public GetRelationsCommandException()
    {
    }

    protected GetRelationsCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public GetRelationsCommandException(string? message) : base(message)
    {
    }

    public GetRelationsCommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}