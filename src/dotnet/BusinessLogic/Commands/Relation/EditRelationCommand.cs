namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using Contracts;
using Database;
using Entities;
using FluentValidation;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Validators;

public class EditRelationCommand(
    IRelationMapper relationMapper,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    IValidator<RelationInput> validator,
    ILogger<EditRelationCommand> logger) : IEditRelationCommand
{
    public async Task EditAsync(RelationInput relationInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await EditBusinessLogicAsync(relationInput, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during {nameof(EditRelationCommand)} command execution.";
            throw new EditRelationCommandException(m, e);
        }
    }

    private async Task EditBusinessLogicAsync(RelationInput relationInput,
        CancellationToken cancellationToken)
    {
        await ValidateInput(relationInput, cancellationToken).ConfigureAwait(false);
        Relation inputRelation = relationMapper.MapRelationInputToRelation(relationInput);
        await EditDatabaseOperationAsync(inputRelation, cancellationToken).ConfigureAwait(false);
    }

    private async Task EditDatabaseOperationAsync(Relation inputRelation, CancellationToken cancellationToken)
    {
        await using DocumentDbContext ctx = new(dbContextOptions);
        Relation toBeModified = await ctx.Relations.FirstAsync(p => p.Id == inputRelation.Id, cancellationToken)
            .ConfigureAwait(false);
        UpdateModifiedValues(toBeModified, inputRelation);
        ctx.Entry(toBeModified).State = EntityState.Modified;
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void UpdateModifiedValues(Relation toBeModified, Relation inputRelation)
    {
        toBeModified.LeftEndStructureNodeId = inputRelation.LeftEndStructureNodeId;
        toBeModified.RightEndStructureNodeId = inputRelation.RightEndStructureNodeId;
    }

    private async Task ValidateInput(RelationInput relationInput, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAsync(relationInput, o =>
        {
            o.IncludeRuleSets(Operations.Update);
            o.ThrowOnFailures();
        }, cancellationToken).ConfigureAwait(false);
    }
}