namespace EncyclopediaGalactica.BusinessLogic.Commands.Document;

using Contracts;
using Database;
using Entities;
using Mappers;
using Microsoft.EntityFrameworkCore;

public class GetAllDocumentsCommand(
    IDocumentMapper documentMapper,
    DbContextOptions<DocumentDbContext> dbContextOptions) : IGetAllDocumentsCommand
{
    /// <inheritdoc />
    public async Task<List<DocumentResult>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAllBusinessLogicAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<List<DocumentResult>> GetAllBusinessLogicAsync(CancellationToken cancellationToken = default)
    {
        List<Document> result = await GetAllDocumentsAsyncDatabaseOperation(cancellationToken).ConfigureAwait(false);
        return documentMapper.MapDocumentsToDocumentResults(result);
    }

    private async Task<List<Document>> GetAllDocumentsAsyncDatabaseOperation(
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        return await ctx.Documents.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}