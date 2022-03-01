namespace Guards;

using System.Runtime.Serialization;

public class GuardAgainstException : Exception
{
    public GuardAgainstException()
    {
    }

    protected GuardAgainstException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public GuardAgainstException(string? message) : base(message)
    {
    }

    public GuardAgainstException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}