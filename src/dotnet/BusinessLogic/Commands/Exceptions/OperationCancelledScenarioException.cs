namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

public class OperationCancelledScenarioException : Exception
{
    public OperationCancelledScenarioException()
    {
    }

    protected OperationCancelledScenarioException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public OperationCancelledScenarioException(string? message) : base(message)
    {
    }

    public OperationCancelledScenarioException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}