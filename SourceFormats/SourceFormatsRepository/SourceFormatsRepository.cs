namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository;

using Interfaces;

public class SourceFormatsRepository : ISourceFormatsRepository
{
    public SourceFormatsRepository(
        ISourceFormatNodeRepository sourceFormatNodeRepository)
    {
        SourceFormatNodes = sourceFormatNodeRepository
                            ?? throw new ArgumentNullException(nameof(sourceFormatNodeRepository));
    }

    public ISourceFormatNodeRepository SourceFormatNodes { get; }
}