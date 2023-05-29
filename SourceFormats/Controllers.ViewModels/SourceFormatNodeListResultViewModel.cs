namespace EncyclopediaGalactica.SourceFormats.Services.Document.ViewModels;

using EncyclopediaGalactica.Services.Document.Dtos;

public class SourceFormatNodeListResultViewModel
{
    public bool IsOperationSuccessful { get; set; }
    public List<SourceFormatNodeDto>? Result { get; set; }
    public string? Message { get; set; }
}