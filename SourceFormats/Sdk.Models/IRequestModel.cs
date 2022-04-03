namespace EncyclopediaGalactica.SourceFormats.Sdk.Models;

public interface IRequestModel<TPayloadType>
{
    public TPayloadType Payload { get; }
}