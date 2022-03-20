namespace Guards;

using System.Runtime.Serialization;

public class GuardValueShouldNoBeNullException : Exception
{
    public GuardValueShouldNoBeNullException()
    {
    }

    protected GuardValueShouldNoBeNullException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public GuardValueShouldNoBeNullException(string? message) : base(message)
    {
    }

    public GuardValueShouldNoBeNullException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}