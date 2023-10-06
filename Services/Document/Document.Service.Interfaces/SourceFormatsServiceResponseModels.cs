namespace EncyclopediaGalactica.Services.Document.Service.Interfaces;

public class SourceFormatsServiceResponseModels<T>
{
    public string Status { get; init; }
    public T? Result { get; init; }
    public bool IsOperationSuccessful { get; init; }
}