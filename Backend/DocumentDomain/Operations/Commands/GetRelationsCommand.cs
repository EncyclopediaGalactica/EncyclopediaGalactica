namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using EncyclopediaGalactica.Common.Contracts;
using Entity;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetRelationsCommand(
    IRelationMapper mapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions,
    ILogger<GetRelationsCommand> logger) : IGetRelationsCommand
{
    public async Task<List<RelationResult>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAllAsyncBusinessLogic(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened while executing {nameof(GetRelationsCommand)} command.";
            throw new GetRelationsCommandException(m, e);
        }
    }

    private async Task<List<RelationResult>> GetAllAsyncBusinessLogic(CancellationToken cancellationToken)
    {
        List<Relation> result = await GetAllAsyncDatabaseOperation(cancellationToken).ConfigureAwait(false);
        return mapper.MapRelationsToRelationResults(result);
    }

    private async Task<List<Relation>> GetAllAsyncDatabaseOperation(CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        return await ctx.Relations.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}