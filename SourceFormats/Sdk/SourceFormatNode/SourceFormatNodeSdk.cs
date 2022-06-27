namespace EncyclopediaGalactica.SourceFormats.Sdk.SourceFormatNode;

using EncyclopediaGalactica.Sdk.Core.Interfaces;
using Interfaces;
using Models.SourceFormatNode;

public partial class SourceFormatNodeSdk : ISourceFormatNodeSdk
{
    private readonly ISdkCore _sdkCore;

    public SourceFormatNodeSdk(ISdkCore sdkCore)
    {
        _sdkCore = sdkCore;
    }

    /// <inheritdoc />
    public async Task<SourceFormatNodeDeleteResponseModel> DeleteAsync(SourceFormatNodeDeleteRequestModel deleteRequestModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}