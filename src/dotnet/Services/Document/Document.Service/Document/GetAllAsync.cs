namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Output;
using Entities;

public partial class DocumentService
{
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