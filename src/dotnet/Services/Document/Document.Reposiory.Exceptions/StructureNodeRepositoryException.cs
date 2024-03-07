namespace EncyclopediaGalactica.Services.Document.Repository.Exceptions;

using System.Runtime.Serialization;

public class StructureNodeRepositoryException : Exception
{
    public StructureNodeRepositoryException()
    {
    }

    protected StructureNodeRepositoryException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public StructureNodeRepositoryException(string? message) : base(message)
    {
    }

    public StructureNodeRepositoryException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}