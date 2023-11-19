namespace EncyclopediaGalactica.Services.Document.Controllers.ViewModels;

using Contracts.Input;

public class SourceFormatNodeSingleResultViewModel
{
    public bool IsOperationSuccessful { get; set; }

    public SourceFormatNodeInputContract? Result { get; set; }

    public string? Message { get; set; }
}