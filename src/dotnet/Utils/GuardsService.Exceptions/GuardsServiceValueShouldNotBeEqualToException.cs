namespace EncyclopediaGalactica.Utils.GuardsService.Exceptions;

using System.Runtime.Serialization;

public class GuardsServiceValueShouldNotBeEqualToException : Exception
{
    public GuardsServiceValueShouldNotBeEqualToException()
    {
    }

    protected GuardsServiceValueShouldNotBeEqualToException(SerializationInfo info, StreamingContext context) : base(
        info,
        context)
    {
    }

    public GuardsServiceValueShouldNotBeEqualToException(string? message) : base(message)
    {
    }

    public GuardsServiceValueShouldNotBeEqualToException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}