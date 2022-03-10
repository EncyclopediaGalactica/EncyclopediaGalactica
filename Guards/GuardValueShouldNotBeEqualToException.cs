namespace Guards;

using System.Runtime.Serialization;

public class GuardValueShouldNotBeEqualToException : Exception
{
    public GuardValueShouldNotBeEqualToException()
    {
    }

    protected GuardValueShouldNotBeEqualToException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public GuardValueShouldNotBeEqualToException(string? message) : base(message)
    {
    }

    public GuardValueShouldNotBeEqualToException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}