namespace EncyclopediaGalactica.Services.Document.Scenario.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     No Such Item Document Service Exception
///     <remarks>
///         It is thrown when the no item has been found based on entity id.
///     </remarks>
/// </summary>
public class NoSuchItemScenarioException : Exception
{
    public NoSuchItemScenarioException()
    {
    }

    protected NoSuchItemScenarioException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoSuchItemScenarioException(string? message) : base(message)
    {
    }

    public NoSuchItemScenarioException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}