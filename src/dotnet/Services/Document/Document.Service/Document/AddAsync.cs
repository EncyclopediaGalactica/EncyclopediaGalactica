namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Input;
using Contracts.Output;
using Entities;
using Errors;
using Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ValidatorService;

public partial class DocumentService
{
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
        _guardsService.NotNull(inputInput);
        await ValidationDocumentInputForAdding(inputInput);
        Document document = _mappers.DocumentMappers.MapDocumentInputToDocument(inputInput);
        Document result = await _repository.AddAsync(document, cancellationToken).ConfigureAwait(false);
        DocumentResult resultInput = _mappers.DocumentMappers.MapDocumentToDocumentResult(result);
        return resultInput;
    }

    private async Task ValidationDocumentInputForAdding(DocumentInput inputInput)
    {
        await _documentDtoValidator.ValidateAsync(inputInput, options =>
        {
            options.IncludeRuleSets(DocumentDtoValidator.Scenarios.AddNew.ToString());
            options.ThrowOnFailures();
        });
    }
}