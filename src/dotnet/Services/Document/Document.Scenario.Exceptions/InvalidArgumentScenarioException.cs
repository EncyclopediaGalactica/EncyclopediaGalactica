namespace EncyclopediaGalactica.Services.Document.Scenario.Exceptions;

using System.Runtime.Serialization;

public class InvalidArgumentScenarioException : Exception
{
    public InvalidArgumentScenarioException()
    {
    }

    protected InvalidArgumentScenarioException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidArgumentScenarioException(string? message) : base(message)
    {
    }

    public InvalidArgumentScenarioException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}