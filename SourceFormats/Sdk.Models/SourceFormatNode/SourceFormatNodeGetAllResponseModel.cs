namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.SourceFormatNode;

using Dtos;
using EncyclopediaGalactica.Sdk.Core.Model.Interfaces;

public class SourceFormatNodeGetAllResponseModel : IResponseModel<List<SourceFormatNodeDto>>
{
    public List<SourceFormatNodeDto>? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public int HttpStatusCode { get; set; }
    public string? Message { get; set; }
}