namespace EncyclopediaGalactica.SourceFormats.Repository.Interfaces;

public interface ISourceFormatsRepository
{
    ISourceFormatNodeRepository SourceFormatNodes { get; }
}