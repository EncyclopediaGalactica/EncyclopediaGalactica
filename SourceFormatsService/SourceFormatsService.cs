namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService;

using Interfaces;

public class SourceFormatsService : ISourceFormatsService
{
    public SourceFormatsService(ISourceFormatNodeService sourceFormatNodeService)
    {
        SourceFormatNodeService =
            sourceFormatNodeService ?? throw new ArgumentNullException(nameof(sourceFormatNodeService));
    }

    public ISourceFormatNodeService SourceFormatNodeService { get; }
}