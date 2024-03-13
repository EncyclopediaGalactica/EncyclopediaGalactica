namespace EncyclopediaGalactica.BusinessLogic.Commands.Exceptions;

using System.Runtime.Serialization;

public class DataCohesionEncyclopediaGalacticaException : Exception
{
    public DataCohesionEncyclopediaGalacticaException()
    {
    }

    protected DataCohesionEncyclopediaGalacticaException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public DataCohesionEncyclopediaGalacticaException(string? message) : base(message)
    {
    }

    public DataCohesionEncyclopediaGalacticaException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}