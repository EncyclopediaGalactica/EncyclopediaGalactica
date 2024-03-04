namespace EncyclopediaGalactica.Services.Document.Mappers;

using Interfaces;

public class SourceFormatMappers : ISourceFormatMappers
{
    public SourceFormatMappers(IDocumentMappers documentMappers)
    {
        ArgumentNullException.ThrowIfNull(documentMappers);

        DocumentMappers = documentMappers;
    }

    public IDocumentMappers DocumentMappers { get; }
}