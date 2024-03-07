namespace EncyclopediaGalactica.Services.Document.Repository;

using Interfaces;

public class SourceFormatsRepository : ISourceFormatsRepository
{
    public SourceFormatsRepository(
        IDocumentsRepository documents)
    {
        ArgumentNullException.ThrowIfNull(documents);

        Documents = documents;
    }

    public IDocumentsRepository Documents { get; }
}