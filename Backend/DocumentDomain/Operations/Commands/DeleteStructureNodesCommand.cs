#region

#endregion

namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands.Exceptions;
using Entity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DeleteStructureNodesCommand(
    DbContextOptions<DocumentDomainDbContext> dbContextOptions,
    ILogger<DeleteStructureNodesCommand> logger) : IDeleteStructureNodesCommand
{
    public async Task DeleteAsync(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ValidateInput(documentId);
            await DeleteAsyncDatabaseOperation(documentId, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task DeleteAsyncDatabaseOperation(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDomainDbContext ctx = new DocumentDomainDbContext(dbContextOptions);
        List<DocumentStructureNode> toBeDeleted = await ctx.DocumentStructureNodes
            .Where(f => f.DocumentId == documentId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        toBeDeleted.ForEach(i => { ctx.Entry(i).State = EntityState.Deleted; });
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void ValidateInput(long documentId)
    {
        const long notAllowedValue = 0;
        if (documentId == notAllowedValue)
        {
            string s = $"{nameof(documentId)} cannot be {notAllowedValue}.";
            throw new InvalidArgumentCommandException(s);
        }
    }
}