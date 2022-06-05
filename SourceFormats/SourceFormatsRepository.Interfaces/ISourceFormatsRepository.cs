namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.Interfaces;

public interface ISourceFormatsRepository
{
    ISourceFormatNodeRepository SourceFormatNodes { get; }
}