#region

#endregion

namespace EncyclopediaGalactica.DocumentDomain.Common.Commands.Exceptions;

using System.Runtime.Serialization;

public class NoSuchItemCommandException : Exception
{
    public NoSuchItemCommandException()
    {
    }

    protected NoSuchItemCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoSuchItemCommandException(string? message) : base(message)
    {
    }

    public NoSuchItemCommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}