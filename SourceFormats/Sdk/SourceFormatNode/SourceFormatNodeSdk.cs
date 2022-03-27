namespace Sdk.SourceFormatNode;

using Core.Interfaces;
using Interfaces;

public partial class SourceFormatNodeSdk : ISourceFormatNodeSdk
{
    private readonly ISdkCore _sdkCore;

    public SourceFormatNodeSdk(ISdkCore sdkCore)
    {
        _sdkCore = sdkCore;
    }
}