#region

#endregion

namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands.Exceptions;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetRelationByIdCommand(
    IRelationMapper mapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions,
    ILogger<GetRelationByIdCommand> logger) : IGetRelationByIdCommand
{
    public async Task<RelationResult> GetByIdAsync(long relationId, CancellationToken cancellationToken)
    {
        try
        {
            return await GetIdByBusinessLogicAsync(relationId, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during {nameof(GetRelationByIdCommand)}.";
            throw new SagaException(m, e);
        }
    }

    private async Task<RelationResult> GetIdByBusinessLogicAsync(long relationId, CancellationToken cancellationToken)
    {
        ValidateInput(relationId);
        Relation result =
            await GetByIdAsyncDatabaseOperation(relationId, cancellationToken).ConfigureAwait(false);
        return mapper.MapRelationToRelationResult(result);
    }

    private async Task<Relation> GetByIdAsyncDatabaseOperation(long relationId,
        CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        return await ctx.Relations.FirstAsync(p => p.Id == relationId, cancellationToken).ConfigureAwait(false);
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