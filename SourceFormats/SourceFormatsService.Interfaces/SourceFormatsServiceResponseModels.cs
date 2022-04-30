namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces;

public class SourceFormatsServiceResponseModels<T>
{
    public string? Message { get; protected init; }
    public SourceFormatsResultStatuses Status { get; init; }
    public T? Result { get; init; }
    public bool IsOperationSuccessful { get; init; }
}