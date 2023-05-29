namespace EncyclopediaGalactica.Services.Document.Sdk.Document;

using EncyclopediaGalactica.Sdk.Core.Interfaces;
using Interfaces;

/// <inheritdoc />
public partial class DocumentSdk : IDocumentsSdk
{
    private readonly ISdkCore _sdkCore;

    public DocumentSdk(ISdkCore sdkCore)
    {
        ArgumentNullException.ThrowIfNull(sdkCore);
        _sdkCore = sdkCore;
    }
}