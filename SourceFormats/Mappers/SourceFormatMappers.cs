namespace EncyclopediaGalactica.SourceFormats.Mappers;

using Interfaces;

public class SourceFormatMappers : ISourceFormatMappers
{
    public SourceFormatMappers(
        ISourceFormatNodeMappers sourceFormatNodeMappers)
    {
        SourceFormatNodeMappers = sourceFormatNodeMappers ??
                                  throw new ArgumentNullException($"{nameof(sourceFormatNodeMappers)} cannot be null.");
    }

    public ISourceFormatNodeMappers SourceFormatNodeMappers { get; }
}