namespace EncyclopediaGalactica.SourceFormats.Sdk.SourceFormatNode;

using Core.Interfaces;
using Interfaces;

public partial class SourceFormatNodeSdk : ISourceFormatNodeSdk
{
    private readonly ISdkCore _sdkCore;

    public SourceFormatNodeSdk(ISdkCore sdkCore)
    {
        _sdkCore = sdkCore;
    }

    private HttpRequestMessage PrepareGet(string url)
    {
        if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException(nameof(url));

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
        return message;
    }
}