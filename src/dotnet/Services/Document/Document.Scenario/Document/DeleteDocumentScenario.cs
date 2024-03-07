namespace EncyclopediaGalactica.Services.Document.Scenario.Document;

using Contracts.Input;
using Errors;
using Exceptions;
using FluentValidation;
using Interfaces.Document;
using Mappers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Repository.Interfaces;
using Utils.GuardsService.Exceptions;
using Utils.GuardsService.Interfaces;

public class DeleteDocumentScenario : IDeleteDocumentScenario
{
    private readonly IValidator<DocumentInput> _documentDtoValidator;
    private readonly IGuardsService _guardsService;
    private readonly ISourceFormatMappers _mappers;
    private readonly IDocumentsRepository _repository;

    public DeleteDocumentScenario(
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
    public async Task DeleteAsync(long documentId, CancellationToken cancellationToken = default)
    {
        try
        {
            await DeleteBusinessLogicAsync(documentId, cancellationToken);
        }
        catch (DocumentNotFoundException e)
        {
            throw new NoSuchItemScenarioException(
                Errors.NoSuchItem,
                e);
        }
        catch (GuardsServiceValueShouldNotBeEqualToException e)
        {
            throw new InvalidInputToDocumentServiceException(
                Errors.InvalidInput,
                e);
        }
        catch (OperationCanceledException e)
        {
            throw new DocumentServiceOperationCancelledException(
                Errors.OperationCancelled,
                e);
        }
        catch (DbUpdateException e)
        {
            throw new UnknownErrorScenarioException(
                Errors.UnexpectedError,
                e);
        }
    }

    private async Task DeleteBusinessLogicAsync(long documentId, CancellationToken cancellationToken)
    {
        ValidateDeleteAsyncInput(documentId);
        await _repository.DeleteAsync(documentId, cancellationToken).ConfigureAwait(false);
    }

    private void ValidateDeleteAsyncInput(long documentId)
    {
        _guardsService.IsNotEqual(documentId, 0);
    }
}