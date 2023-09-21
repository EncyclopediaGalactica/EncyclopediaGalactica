namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;

using Errors;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using SourceFormatsRepository.Exceptions;
using Utils.GuardsService.Exceptions;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task DeleteAsync(long documentId, CancellationToken cancellationToken = default)
    {
        try
        {
            await DeleteBusinessLogicAsync(documentId, cancellationToken);
        }
        catch (DocumentNotFoundException e)
        {
            throw new NoSuchItemDocumentServiceException(
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
            throw new UnknownErrorDocumentServiceException(
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