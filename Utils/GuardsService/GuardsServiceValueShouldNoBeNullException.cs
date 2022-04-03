namespace EncyclopediaGalactica.Utils.GuardsService;

using System.Runtime.Serialization;

public class GuardsServiceValueShouldNoBeNullException : Exception
{
    public GuardsServiceValueShouldNoBeNullException()
    {
    }

    protected GuardsServiceValueShouldNoBeNullException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public GuardsServiceValueShouldNoBeNullException(string? message) : base(message)
    {
    }

    public GuardsServiceValueShouldNoBeNullException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}