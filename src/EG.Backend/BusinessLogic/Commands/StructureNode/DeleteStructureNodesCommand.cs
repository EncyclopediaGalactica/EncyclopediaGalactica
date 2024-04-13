namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Database;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DeleteStructureNodesCommand(
    DbContextOptions<DocumentDbContext> dbContextOptions,
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
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        List<StructureNode> toBeDeleted = await ctx.StructureNodes.Where(f => f.DocumentId == documentId)
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