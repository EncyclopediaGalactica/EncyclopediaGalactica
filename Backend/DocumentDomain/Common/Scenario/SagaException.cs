namespace EncyclopediaGalactica.DocumentDomain.Common.Scenario;

using System.Runtime.Serialization;

public class SagaException : Exception
{
    public SagaException()
    {
    }

    protected SagaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public SagaException(string? message) : base(message)
    {
    }

    public SagaException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}