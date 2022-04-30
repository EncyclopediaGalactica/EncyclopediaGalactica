namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService;

using Interfaces;
using Interfaces.SourceFormatNode;

public class SourceFormatsService : ISourceFormatsService
{
    public SourceFormatsService(ISourceFormatNodeService sourceFormatNodeService)
    {
        SourceFormatNode =
            sourceFormatNodeService ?? throw new ArgumentNullException(nameof(sourceFormatNodeService));
    }

    public ISourceFormatNodeService SourceFormatNode { get; }
}