namespace EncyclopediaGalactica.Services.Document.Controllers.ViewModels;

using Contracts.Input;

public class SourceFormatNodeListResultViewModel
{
    public bool IsOperationSuccessful { get; set; }
    public List<SourceFormatNodeInputContract>? Result { get; set; }
    public string? Message { get; set; }
}