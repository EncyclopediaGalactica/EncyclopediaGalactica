namespace EncyclopediaGalactica.Services.Document.Service.Document;

using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.Errors;
using EncyclopediaGalactica.Services.Document.Repository.Exceptions;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService.Exceptions;
using Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<DocumentDto> UpdateAsync(long documentId, DocumentDto modifiedDto)
    {
        try
        {
            return await UpdateBusinessLogicAsync(documentId, modifiedDto);
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

    private async Task<DocumentDto> UpdateBusinessLogicAsync(long documentId, DocumentDto modifiedDto)
    {
        _guardsService.IsNotEqual(documentId, 0);
        _guardsService.NotNull(modifiedDto);

        await ValidateUpdateAsyncInput(modifiedDto).ConfigureAwait(false);
        Document mappedDocument = _mappers.DocumentMappers.MapDocumentDtoToDocument(modifiedDto);
        Document updateDocument = await _repository.UpdateAsync(
            documentId,
            mappedDocument).ConfigureAwait(false);
        DocumentDto updatedAndMappedDocumentDto = _mappers.DocumentMappers.MapDocumentToDocumentDto(updateDocument);
        return updatedAndMappedDocumentDto;
    }

    private async Task ValidateUpdateAsyncInput(DocumentDto modifiedDto)
    {
        await _documentDtoValidator.ValidateAsync(modifiedDto, o =>
        {
            o.IncludeRuleSets(DocumentDtoValidator.Scenarios.Update.ToString());
            o.ThrowOnFailures();
        });
    }
}