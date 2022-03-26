namespace Sdk.SourceFormatNode;

using Interfaces;

public partial class SourceFormatNodeSdk : ISourceFormatNodeSdk
{
    private readonly HttpClient _httpClient;

    public SourceFormatNodeSdk(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}