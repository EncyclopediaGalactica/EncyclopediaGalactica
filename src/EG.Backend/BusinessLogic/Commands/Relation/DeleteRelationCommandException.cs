namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using System.Runtime.Serialization;

public class DeleteRelationCommandException : Exception
{
    public DeleteRelationCommandException()
    {
    }

    protected DeleteRelationCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DeleteRelationCommandException(string? message) : base(message)
    {
    }

    public DeleteRelationCommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}