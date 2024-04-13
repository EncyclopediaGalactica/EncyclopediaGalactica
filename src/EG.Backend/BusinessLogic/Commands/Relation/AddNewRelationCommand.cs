namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using Contracts;
using Database;
using Entities;
using FluentValidation;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sagas;
using Validators;

public class AddNewRelationCommand(
    IValidator<RelationInput> validator,
    IRelationMapper mapper,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    ILogger<AddNewRelationCommand> logger) : IAddNewRelationCommand
{
    public async Task<long> AddNewRelationAsync(RelationInput payload, CancellationToken cancellationToken)
    {
        try
        {
            return await AddNewBusinessLogicAsync(payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during {nameof(AddNewRelationCommand)}.";
            throw new SagaException(m, e);
        }
    }

    private async Task<long> AddNewBusinessLogicAsync(RelationInput payload, CancellationToken cancellationToken)
    {
        await ValidateInput(payload, cancellationToken).ConfigureAwait(false);
        Relation relation = mapper.MapRelationInputToRelation(payload);
        return await AddNewDatabaseOperationAsync(relation, cancellationToken).ConfigureAwait(false);
    }

    private async Task<long> AddNewDatabaseOperationAsync(Relation relation,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        await ctx.Relations.AddAsync(relation, cancellationToken).ConfigureAwait(false);
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return relation.Id;
    }

    private async Task ValidateInput(RelationInput payload, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAsync(payload, o =>
        {
            o.IncludeRuleSets(Operations.Add);
            o.ThrowOnFailures();
        }, cancellationToken).ConfigureAwait(false);
    }
}