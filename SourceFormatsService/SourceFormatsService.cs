namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService;

using Guards;
using Interfaces;

public class SourceFormatsService : ISourceFormatsService
{
    public SourceFormatsService(ISourceFormatNodeService sourceFormatNodeService)
    {
        Guard.NotNull(sourceFormatNodeService);

        SourceFormatNodeService = sourceFormatNodeService;
    }

    public ISourceFormatNodeService SourceFormatNodeService { get; }
}