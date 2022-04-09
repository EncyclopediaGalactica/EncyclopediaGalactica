namespace EncyclopediaGalactica.Sdk.Core.Model.Interfaces;

public interface IRequestModel<TPayloadType>
{
    public TPayloadType? Payload { get; }
}