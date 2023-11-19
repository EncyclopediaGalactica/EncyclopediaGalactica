namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Contracts.Input;
using Entities;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<List<DocumentGraphqlInput>> GetAllAsync(CancellationToken cancellationToken = default)
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

    private async Task<List<DocumentGraphqlInput>> GetAllBusinessLogicAsync()
    {
        List<Document> result = await _repository.GetAllAsync().ConfigureAwait(false);
        List<DocumentGraphqlInput> mappedResult = _mappers.DocumentMappers.MapDocumentsToDocumentDtos(result);
        return mappedResult;
    }
}