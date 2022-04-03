namespace EncyclopediaGalactica.SourceFormats.Sdk.Models;

public interface IResponseModel<TResponsePayloadType>
{
    public TResponsePayloadType? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public int HttpStatusCode { get; set; }
    public string? Message { get; set; }
}