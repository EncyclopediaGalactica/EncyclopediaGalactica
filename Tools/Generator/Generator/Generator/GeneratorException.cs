namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

public class GeneratorException : Exception
{
    public GeneratorException()
    {
    }

    public GeneratorException(string? message) : base(message)
    {
    }

    public GeneratorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}