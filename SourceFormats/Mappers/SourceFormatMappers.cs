namespace EncyclopediaGalactica.SourceFormats.Mappers;

using Interfaces;

public class SourceFormatMappers : ISourceFormatMappers
{
    public SourceFormatMappers(
        ISourceFormatNodeMappers sourceFormatNodeMappers)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatNodeMappers);

        SourceFormatNodeMappers = sourceFormatNodeMappers;
    }

    public ISourceFormatNodeMappers SourceFormatNodeMappers { get; }
}