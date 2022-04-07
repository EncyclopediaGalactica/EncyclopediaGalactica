namespace EncyclopediaGalactica.SourceFormats.Sdk.Core.Interfaces;

public interface ISdkCore
{
    HttpRequestMessage PreparePost(object obj, string url);

    Task<TResponseModel> SendAsync<TResponseModel, TResponsePayload>(
        HttpRequestMessage message,
        CancellationToken cancellationToken = default)
        where TResponseModel : new()
        where TResponsePayload : new();
}