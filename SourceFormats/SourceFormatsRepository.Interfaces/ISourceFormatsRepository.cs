namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;

public interface ISourceFormatsRepository
{
    ISourceFormatNodeRepository SourceFormatNodes { get; }
    IDocumentsRepository Documents { get; }
}