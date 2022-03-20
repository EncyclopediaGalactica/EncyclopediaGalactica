namespace EncyclopediaGalactica.SourceFormats.SourceFormatsCacheService;

using Interfaces;

public class SourceFormatsCacheService : ISourceFormatsCacheService
{
    public SourceFormatsCacheService(ISourceFormatNodeCacheService sourceFormatNodeCacheService)
    {
        SourceFormatNodeCacheService = sourceFormatNodeCacheService ??
                                       throw new ArgumentNullException(nameof(sourceFormatNodeCacheService));
    }

    public ISourceFormatNodeCacheService SourceFormatNodeCacheService { get; }
}