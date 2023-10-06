namespace EncyclopediaGalactica.Services.Document.Service.Document;

using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Entities;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<List<DocumentDto>> GetAllAsync(CancellationToken cancellationToken = default)
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

    private async Task<List<DocumentDto>> GetAllBusinessLogicAsync()
    {
        List<Document> result = await _repository.GetAllAsync().ConfigureAwait(false);
        List<DocumentDto> mappedResult = _mappers.DocumentMappers.MapDocumentsToDocumentDtos(result);
        return mappedResult;
    }
}