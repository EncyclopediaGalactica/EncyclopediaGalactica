namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;

using EncyclopediaGalactica.Common.Contracts;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class GetRelationsScenario(
    DbContextOptions<DocumentDomainDbContext> dbContextOptions,
    RelationMapper relationMapper
)
{
    private Guid _correlationId;

    public async Task<Either<ErrorResult, List<RelationResult>>> ExecuteAsync(
        GetRelationsScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        _correlationId = context.correlationId;
        Either<ErrorResult, List<RelationResult>> result =
            from entities in FetchFromDatabase(cancellationToken)
            from mappedEntities in MapToRelationResult(entities)
            select mappedEntities;
        return result;
    }

    private Either<ErrorResult, List<RelationResult>> MapToRelationResult(List<Entity.Relation> entities)
    {
        return relationMapper.MapRelationsToRelationResults(entities);
    }

    private Either<ErrorResult, List<Entity.Relation>> FetchFromDatabase(
        CancellationToken cancellationToken)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            List<Entity.Relation> result = ctx.Relations.ToList();
            return result;
        }
        catch (Exception e)
        {
            ErrorResult errorDetails = new ErrorResult(_correlationId, e.Message);
            return errorDetails;
        }
    }
}

public record GetRelationsScenarioContext(Guid correlationId);