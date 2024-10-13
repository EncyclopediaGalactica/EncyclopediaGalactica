#region

#endregion

namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands;
using Common.Commands.Exceptions;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

public class GetAllDocumentsCommand(
    IDocumentMapper documentMapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions) : IGetAllDocumentsCommand
{
    /// <inheritdoc />
    public async Task<List<DocumentResult>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAllBusinessLogicAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException e)
        {
            string m = $"{nameof(GetAllDocumentsCommand)} execution has been cancelled.";
            throw new OperationCancelledCommandException(m, e);
        }
        catch (Exception e)
        {
            throw new UnknownErrorCommandException(
                Errors.UnexpectedError,
                e);
        }
    }

    private async Task<List<DocumentResult>> GetAllBusinessLogicAsync(CancellationToken cancellationToken = default)
    {
        List<Document> result =
            await GetAllDocumentsAsyncDatabaseOperation(cancellationToken).ConfigureAwait(false);
        return documentMapper.MapDocumentsToDocumentResults(result);
    }

    private async Task<List<Document>> GetAllDocumentsAsyncDatabaseOperation(
        CancellationToken cancellationToken = default)
    {
        await using DocumentDomainDbContext ctx = new DocumentDomainDbContext(dbContextOptions);
        return await ctx.Documents.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}