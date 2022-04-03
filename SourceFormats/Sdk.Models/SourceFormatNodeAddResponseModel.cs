namespace EncyclopediaGalactica.SourceFormats.Sdk.Models;

using Dtos;

public class SourceFormatNodeAddResponseModel : IResponseModel<SourceFormatNodeDto>
{
    public SourceFormatNodeDto? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public int HttpStatusCode { get; set; }
    public string? Message { get; set; }
}