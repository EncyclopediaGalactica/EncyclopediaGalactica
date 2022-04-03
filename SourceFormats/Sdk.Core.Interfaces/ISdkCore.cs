namespace EncyclopediaGalactica.SourceFormats.Sdk.Core.Interfaces;

public interface ISdkCore
{
    HttpRequestMessage PreparePost(object obj, string url);
    Task<T> SendAsync<T>(HttpRequestMessage message, CancellationToken cancellationToken = default) where T : new();
}