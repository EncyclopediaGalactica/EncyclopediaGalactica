namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using Database;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DeleteRelationCommand(
    DbContextOptions<DocumentDbContext> dbContextOptions,
    ILogger<DeleteRelationCommand> logger) : IDeleteRelationCommand
{
    public async Task DeleteAsync(long relationId, CancellationToken cancellationToken)
    {
        try
        {
            await DeleteBusinessLogicAsync(relationId, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during execution of {nameof(DeleteRelationCommand)} command.";
            throw new DeleteRelationCommandException(m, e);
        }
    }

    private async Task DeleteBusinessLogicAsync(long relationId, CancellationToken cancellationToken)
    {
        ValidateInput(relationId);
        await DatabaseOperationAsync(relationId, cancellationToken).ConfigureAwait(false);
    }

    private async Task DatabaseOperationAsync(long relationId, CancellationToken cancellationToken)
    {
        await using DocumentDbContext ctx = new(dbContextOptions);
        Relation toBeDeleted = await ctx.Relations.FirstAsync(f => f.Id == relationId, cancellationToken)
            .ConfigureAwait(false);
        ctx.Entry(toBeDeleted).State = EntityState.Deleted;
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void ValidateInput(long relationId)
    {
        long notAllowedValue = 0;
        if (relationId == notAllowedValue)
        {
            string m = $"{nameof(relationId)} cannot be {notAllowedValue}";
            throw new InvalidArgumentCommandException(m);
        }
    }
}