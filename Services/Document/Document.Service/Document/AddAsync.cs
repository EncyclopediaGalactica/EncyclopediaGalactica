using Errors = Document.Errors.Errors;

namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;

using Dtos;
using Entities;
using Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ValidatorService;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<DocumentDto> AddAsync(DocumentDto dtoInput, CancellationToken cancellationToken = default)
    {
        try
        {
            _guardsService.NotNull(dtoInput);

            return await AddBusinessLogicAsync(dtoInput, cancellationToken);
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

    private async Task<DocumentDto> AddBusinessLogicAsync(DocumentDto dtoInput, CancellationToken cancellationToken)
    {
        await ValidationDocumentInputForAdding(dtoInput);
        Document document = _mappers.DocumentMappers.MapDocumentDtoToDocument(dtoInput);
        Document result = await _repository.AddAsync(document, cancellationToken).ConfigureAwait(false);
        DocumentDto resultDto = _mappers.DocumentMappers.MapDocumentToDocumentDto(result);
        return resultDto;
    }

    private async Task ValidationDocumentInputForAdding(DocumentDto dtoInput)
    {
        await _documentDtoValidator.ValidateAsync(dtoInput, options =>
        {
            options.IncludeRuleSets(DocumentDtoValidator.Scenarios.AddNew.ToString());
            options.ThrowOnFailures();
        });
    }
}