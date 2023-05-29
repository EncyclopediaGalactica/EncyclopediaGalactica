namespace EncyclopediaGalactica.SourceFormats.Services.Document.ViewModels;

using EncyclopediaGalactica.Services.Document.Dtos;

public class SourceFormatNodeSingleResultViewModel
{
    public bool IsOperationSuccessful { get; set; }

    public SourceFormatNodeDto? Result { get; set; }

    public string? Message { get; set; }
}