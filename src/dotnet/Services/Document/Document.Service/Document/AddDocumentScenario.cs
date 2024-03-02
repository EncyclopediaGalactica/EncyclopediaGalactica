namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Input;
using Contracts.Output;
using Entities;
using Errors;
using Exceptions;
using FluentValidation;
using Interfaces.Document;
using Mappers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Utils.GuardsService.Interfaces;
using ValidatorService;

public class AddDocumentScenario : IAddDocumentScenario
{
    
    private readonly IValidator<DocumentInput> _documentDtoValidator;
    private readonly IGuardsService _guardsService;
    private readonly ISourceFormatMappers _mappers;
    private readonly IDocumentsRepository _repository;

    public AddDocumentScenario(
        IGuardsService guardsService,
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository,
        IValidator<DocumentInput> documentDtoValidator)
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
    
    /// <inheritdoc />
    public async Task<DocumentResult> AddAsync(DocumentInput inputInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddBusinessLogicAsync(inputInput, cancellationToken);
        }
        catch (Exception e) when (e is ArgumentNullException
                                      or ValidationException
                                      or DbUpdateException)
        {
            throw new InvalidInputToDocumentServiceException(
                Errors.InvalidInput,
                e);
        }
        catch (Exception e) when (e is OperationCanceledException)
        {
            throw new DocumentServiceOperationCancelledException(
                Errors.OperationCancelled,
                e);
        }
        catch (Exception e)
        {
            throw new UnknownErrorDocumentServiceException(
                Errors.UnexpectedError,
                e);
        }
    }

    private async Task<DocumentResult> AddBusinessLogicAsync(DocumentInput inputInput,
        CancellationToken cancellationToken)
    {
        await ValidationDocumentInputForAdding(inputInput);
        Document document = _mappers.DocumentMappers.MapDocumentInputToDocument(inputInput);
        Document result = await _repository.AddAsync(document, cancellationToken).ConfigureAwait(false);
        DocumentResult resultInput = _mappers.DocumentMappers.MapDocumentToDocumentResult(result);
        return resultInput;
    }

    private async Task ValidationDocumentInputForAdding(DocumentInput inputInput)
    {
        _guardsService.NotNull(inputInput);
        await _documentDtoValidator.ValidateAsync(inputInput, options =>
        {
            options.IncludeRuleSets(DocumentInputValidator.Scenarios.AddNew.ToString());
            options.ThrowOnFailures();
        });
    }
}