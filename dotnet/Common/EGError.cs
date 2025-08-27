namespace EncyclopediaGalactica.Common;

public record EgError
{
    public EgError(string message) => Message = message;

    public EgError(string message, string? trace)
    {
        Message = message;
        Trace = trace;
    }

    public string Message { get; set; }
    public string Trace { get; set; }
}