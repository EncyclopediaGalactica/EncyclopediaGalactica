namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Document;

using Dtos;
using Entities;
using Interfaces.Document;
using Mappers.Interfaces;
using Utils.GuardsService.Interfaces;

/// <inheritdoc />
public partial class DocumentService : IDocumentService
{
    private readonly IGuardsService _guardsService;
    private readonly ISourceFormatMappers _mappers;

    public DocumentService(
        IGuardsService guardsService,
        ISourceFormatMappers mappers)
    {
        ArgumentNullException.ThrowIfNull(guardsService);
        ArgumentNullException.ThrowIfNull(mappers);
        _guardsService = guardsService;
        _mappers = mappers;
    }

    public async Task<List<Document>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Document> GetById(long id)
    {
        throw new NotImplementedException();
    }
}