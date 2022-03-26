namespace Sdk;

using Interfaces;

public class SourceFormatsSdkSdk : ISourceFormatsSdk
{
    public SourceFormatsSdkSdk(ISourceFormatNodeSdk sourceFormatNodeSdk)
    {
        SourceFormatNodeSdk = sourceFormatNodeSdk;
    }

    public ISourceFormatNodeSdk SourceFormatNodeSdk { get; }
}