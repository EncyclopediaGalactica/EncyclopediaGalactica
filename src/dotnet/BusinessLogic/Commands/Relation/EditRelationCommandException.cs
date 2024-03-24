namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using System.Runtime.Serialization;

public class EditRelationCommandException : Exception
{
    public EditRelationCommandException()
    {
    }

    protected EditRelationCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public EditRelationCommandException(string? message) : base(message)
    {
    }

    public EditRelationCommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}