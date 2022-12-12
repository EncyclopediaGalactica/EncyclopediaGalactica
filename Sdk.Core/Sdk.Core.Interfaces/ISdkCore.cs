namespace EncyclopediaGalactica.Sdk.Core.Interfaces;

using Model.Interfaces;

public interface ISdkCore
{
    Task<TResponseModel> SendAsync<TResponseModel, TResponseModelPayload>(
        HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken = default)
        where TResponseModel : IHttpResponseModel<TResponseModelPayload>, new();
}