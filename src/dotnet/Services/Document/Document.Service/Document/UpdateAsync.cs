namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Input;
using Contracts.Output;
using Entities;
using Errors;
using Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Utils.GuardsService.Exceptions;
using ValidatorService;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<DocumentResult> UpdateAsync(long documentId, DocumentInput modifiedInput)
    {
        try
        {
            return await UpdateBusinessLogicAsync(documentId, modifiedInput);
        }
        catch (Exception e) when (e is GuardsServiceValueShouldNotBeEqualToException
                                      or GuardsServiceValueShouldNoBeNullException
                                      or DbUpdateException
                                      or ValidationException)
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
        catch (Exception e) when (e is DocumentNotFoundException)
        {
            throw new NoSuchItemDocumentServiceException(
                Errors.NoSuchItem,
                e);
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException
                                      or not null)
        {
            throw new UnknownErrorDocumentServiceException(
                Errors.UnexpectedError,
                e);
        }
    }

    private async Task<DocumentResult> UpdateBusinessLogicAsync(long documentId,
        DocumentInput modifiedInput)
    {
        _guardsService.IsNotEqual(documentId, 0);
        _guardsService.NotNull(modifiedInput);

        await ValidateUpdateAsyncInput(modifiedInput).ConfigureAwait(false);
        Document mappedDocument = _mappers.DocumentMappers.MapDocumentInputToDocument(modifiedInput);
        Document updateDocument = await _repository.UpdateAsync(
            documentId,
            mappedDocument).ConfigureAwait(false);
        DocumentResult updatedAndMappedDocumentInput =
            _mappers.DocumentMappers.MapDocumentToDocumentResult(updateDocument);
        return updatedAndMappedDocumentInput;
    }

    private async Task ValidateUpdateAsyncInput(DocumentInput modifiedInput)
    {
        await _documentDtoValidator.ValidateAsync(modifiedInput, o =>
        {
            o.IncludeRuleSets(DocumentDtoValidator.Scenarios.Update.ToString());
            o.ThrowOnFailures();
        });
    }
}