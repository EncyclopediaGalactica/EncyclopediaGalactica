namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Input;
using Entities;
using Errors;
using Exceptions;
using Utils.GuardsService.Exceptions;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<DocumentGraphqlInput> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _guardsService.IsNotEqual(id, 0);
            return await GetByIdBusinessLogicAsync(id, cancellationToken);
        }
        catch (Exception e) when (e is ArgumentNullException
                                      or GuardsServiceValueShouldNotBeEqualToException)
        {
            throw new InvalidInputToDocumentServiceException(
                Errors.InvalidInput,
                e);
        }
        catch (Exception e) when (e is InvalidOperationException)
        {
            throw new NoSuchItemDocumentServiceException(
                Errors.NoSuchItem,
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

    private async Task<DocumentGraphqlInput> GetByIdBusinessLogicAsync(long id, CancellationToken cancellationToken)
    {
        Document result = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        DocumentGraphqlInput graphqlInput = _mappers.DocumentMappers.MapDocumentToDocumentDto(result);
        return graphqlInput;
    }
}