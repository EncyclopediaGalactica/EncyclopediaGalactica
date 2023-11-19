namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Input;
using Entities;
using Errors;
using Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ValidatorService;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<DocumentGraphqlInput> AddAsync(DocumentGraphqlInput graphqlInputGraphqlInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddBusinessLogicAsync(graphqlInputGraphqlInput, cancellationToken);
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

    private async Task<DocumentGraphqlInput> AddBusinessLogicAsync(DocumentGraphqlInput graphqlInputGraphqlInput,
        CancellationToken cancellationToken)
    {
        _guardsService.NotNull(graphqlInputGraphqlInput);
        await ValidationDocumentInputForAdding(graphqlInputGraphqlInput);
        Document document = _mappers.DocumentMappers.MapDocumentDtoToDocument(graphqlInputGraphqlInput);
        Document result = await _repository.AddAsync(document, cancellationToken).ConfigureAwait(false);
        DocumentGraphqlInput resultGraphqlInput = _mappers.DocumentMappers.MapDocumentToDocumentDto(result);
        return resultGraphqlInput;
    }

    private async Task ValidationDocumentInputForAdding(DocumentGraphqlInput graphqlInputGraphqlInput)
    {
        await _documentDtoValidator.ValidateAsync(graphqlInputGraphqlInput, options =>
        {
            options.IncludeRuleSets(DocumentDtoValidator.Scenarios.AddNew.ToString());
            options.ThrowOnFailures();
        });
    }
}