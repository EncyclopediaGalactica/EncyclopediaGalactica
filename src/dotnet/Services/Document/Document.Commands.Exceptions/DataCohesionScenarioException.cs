namespace EncyclopediaGalactica.Services.Document.Scenario.Exceptions;

using System.Runtime.Serialization;

public class DataCohesionScenarioException : Exception
{
    public DataCohesionScenarioException()
    {
    }

    protected DataCohesionScenarioException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DataCohesionScenarioException(string? message) : base(message)
    {
    }

    public DataCohesionScenarioException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}