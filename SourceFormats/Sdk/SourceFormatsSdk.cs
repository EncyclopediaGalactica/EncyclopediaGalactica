namespace EncyclopediaGalactica.SourceFormats.Sdk;

using Interfaces;

public class SourceFormatsSdk : ISourceFormatsSdk
{
    public SourceFormatsSdk(ISourceFormatNodeSdk sourceFormatNodeSdk)
    {
        SourceFormatNode = sourceFormatNodeSdk;
    }

    public ISourceFormatNodeSdk SourceFormatNode { get; }
}