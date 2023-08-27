namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;

using Interfaces.Document;
using Mappers.Interfaces;
using SourceFormatsRepository.Interfaces;
using Utils.GuardsService.Interfaces;

/// <inheritdoc />
public partial class DocumentService : IDocumentService
{
    private readonly IGuardsService _guardsService;
    private readonly ISourceFormatMappers _mappers;
    private readonly IDocumentsRepository _repository;

    public DocumentService(
        IGuardsService guardsService,
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository)
    {
        ArgumentNullException.ThrowIfNull(guardsService);
        ArgumentNullException.ThrowIfNull(mappers);
        ArgumentNullException.ThrowIfNull(documentsRepository);

        _guardsService = guardsService;
        _mappers = mappers;
        _repository = documentsRepository;
    }
}