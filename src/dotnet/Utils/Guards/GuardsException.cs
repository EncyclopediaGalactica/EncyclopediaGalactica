namespace EncyclopediaGalactica.Utils.Guards;

using System.Runtime.Serialization;

public class GuardsException : Exception
{
    public GuardsException()
    {
    }

    protected GuardsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public GuardsException(string? message) : base(message)
    {
    }

    public GuardsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}