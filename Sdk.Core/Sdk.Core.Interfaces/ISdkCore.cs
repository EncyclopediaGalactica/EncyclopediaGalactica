namespace EncyclopediaGalactica.Sdk.Core.Interfaces;

using Model.Interfaces;

public interface ISdkCore
{
    HttpRequestMessage PreparePost(object obj, string url);

    Task<IResponseModel<TResponseModelPayload>> SendAsync<TResponseModel, TResponseModelPayload>(
        HttpRequestMessage message,
        CancellationToken cancellationToken = default);
}