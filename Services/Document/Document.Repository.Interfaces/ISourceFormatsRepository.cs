namespace EncyclopediaGalactica.Services.Document.Repository.Interfaces;

public interface ISourceFormatsRepository
{
    ISourceFormatNodeRepository SourceFormatNodes { get; }
    IDocumentsRepository Documents { get; }
}