namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Output;
using Entities;
using Errors;
using Exceptions;
using Interfaces.Document;
using Mappers.Interfaces;
using Repository.Interfaces;
using Utils.GuardsService.Exceptions;
using Utils.GuardsService.Interfaces;

public class GetDocumentByIdScenario : IGetDocumentByIdScenario
{
    private readonly ISourceFormatMappers _mappers;
    private readonly IDocumentsRepository _repository;
    private readonly IGuardsService _guardsService;

    public GetDocumentByIdScenario(
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository,
        IGuardsService guardsService)
    {
        ArgumentNullException.ThrowIfNull(mappers);
        ArgumentNullException.ThrowIfNull(documentsRepository);
        ArgumentNullException.ThrowIfNull(guardsService);

        _mappers = mappers;
        _repository = documentsRepository;
        _guardsService = guardsService;
    }
    
    /// <inheritdoc />
    public async Task<DocumentResult> GetByIdAsync(
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

    private async Task<DocumentResult> GetByIdBusinessLogicAsync(long id, CancellationToken cancellationToken)
    {
        Document result = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        DocumentResult input = _mappers.DocumentMappers.MapDocumentToDocumentResult(result);
        return input;
    }
}