namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Input;
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
    public async Task<DocumentGraphqlInput> UpdateAsync(long documentId, DocumentGraphqlInput modifiedGraphqlInput)
    {
        try
        {
            return await UpdateBusinessLogicAsync(documentId, modifiedGraphqlInput);
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

    private async Task<DocumentGraphqlInput> UpdateBusinessLogicAsync(long documentId,
        DocumentGraphqlInput modifiedGraphqlInput)
    {
        _guardsService.IsNotEqual(documentId, 0);
        _guardsService.NotNull(modifiedGraphqlInput);

        await ValidateUpdateAsyncInput(modifiedGraphqlInput).ConfigureAwait(false);
        Document mappedDocument = _mappers.DocumentMappers.MapDocumentDtoToDocument(modifiedGraphqlInput);
        Document updateDocument = await _repository.UpdateAsync(
            documentId,
            mappedDocument).ConfigureAwait(false);
        DocumentGraphqlInput updatedAndMappedDocumentGraphqlInput =
            _mappers.DocumentMappers.MapDocumentToDocumentDto(updateDocument);
        return updatedAndMappedDocumentGraphqlInput;
    }

    private async Task ValidateUpdateAsyncInput(DocumentGraphqlInput modifiedGraphqlInput)
    {
        await _documentDtoValidator.ValidateAsync(modifiedGraphqlInput, o =>
        {
            o.IncludeRuleSets(DocumentDtoValidator.Scenarios.Update.ToString());
            o.ThrowOnFailures();
        });
    }
}