namespace EncyclopediaGalactica.Services.Document.Scenario.Document;

using Contracts.Output;
using Entities;
using Interfaces.Document;
using Mappers.Interfaces;
using Repository.Interfaces;

public class GetAllDocumentsScenario : IGetAllDocumentsScenario
{
    private readonly ISourceFormatMappers _mappers;
    private readonly IDocumentsRepository _repository;

    public GetAllDocumentsScenario(
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository)
    {
        ArgumentNullException.ThrowIfNull(mappers);
        ArgumentNullException.ThrowIfNull(documentsRepository);

        _mappers = mappers;
        _repository = documentsRepository;
    }

    /// <inheritdoc />
    public async Task<List<DocumentResult>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAllBusinessLogicAsync().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<List<DocumentResult>> GetAllBusinessLogicAsync()
    {
        List<Document> result = await _repository.GetAllAsync().ConfigureAwait(false);
        List<DocumentResult> mappedResult = _mappers.DocumentMappers.MapDocumentsToDocumentResults(result);
        return mappedResult;
    }
}