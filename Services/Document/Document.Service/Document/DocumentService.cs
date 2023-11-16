namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Dtos;
using FluentValidation;
using Interfaces.Document;
using Mappers.Interfaces;
using Repository.Interfaces;
using Utils.GuardsService.Interfaces;

/// <inheritdoc />
public partial class DocumentService : IDocumentService
{
    private readonly IValidator<DocumentDto> _documentDtoValidator;
    private readonly IGuardsService _guardsService;
    private readonly ISourceFormatMappers _mappers;
    private readonly IDocumentsRepository _repository;

    public DocumentService(
        IGuardsService guardsService,
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository,
        IValidator<DocumentDto> documentDtoValidator)
    {
        ArgumentNullException.ThrowIfNull(guardsService);
        ArgumentNullException.ThrowIfNull(mappers);
        ArgumentNullException.ThrowIfNull(documentsRepository);
        ArgumentNullException.ThrowIfNull(documentDtoValidator);

        _guardsService = guardsService;
        _mappers = mappers;
        _repository = documentsRepository;
        _documentDtoValidator = documentDtoValidator;
    }
}