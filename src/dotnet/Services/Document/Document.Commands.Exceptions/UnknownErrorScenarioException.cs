namespace EncyclopediaGalactica.Services.Document.Scenario.Exceptions;

using System.Runtime.Serialization;

/// <summary>
///     Unknown Error At Document Service Exception
///     <remarks>
///         It is thrown when an unknown error happens in the service.
///     </remarks>
/// </summary>
public class UnknownErrorScenarioException : Exception
{
    public UnknownErrorScenarioException()
    {
    }

    protected UnknownErrorScenarioException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public UnknownErrorScenarioException(string? message) : base(message)
    {
    }

    public UnknownErrorScenarioException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}