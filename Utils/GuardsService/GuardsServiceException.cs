namespace EncyclopediaGalactica.Utils.GuardsService;

using System.Runtime.Serialization;

public class GuardsServiceException : Exception
{
    public GuardsServiceException()
    {
    }

    protected GuardsServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public GuardsServiceException(string? message) : base(message)
    {
    }

    public GuardsServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}