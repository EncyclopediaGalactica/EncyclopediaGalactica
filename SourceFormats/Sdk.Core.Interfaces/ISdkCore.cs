namespace Sdk.Core.Interfaces;

public interface ISdkCore
{
    HttpRequestMessage PreparePost(object dto, string url);
    Task<T?> SendAsync<T>(HttpRequestMessage message, CancellationToken cancellationToken = default);
}