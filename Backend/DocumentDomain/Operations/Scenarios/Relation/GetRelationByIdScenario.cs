namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;

using EncyclopediaGalactica.Common.Contracts;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class GetRelationByIdScenario(
    RelationMapper mapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    private Guid _correlationId;

    public async Task<Either<ErrorResult, RelationResult>> ExecuteAsync(
        GetRelationByIdScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        _correlationId = context.CorrelationId;
        Either<ErrorResult, RelationResult> result =
            from entity in FetchFromDatabase(context, cancellationToken)
            from mappedEntity in MapToRelationResult(entity)
            select mappedEntity;
        return result;
    }

    private Either<ErrorResult, RelationResult> MapToRelationResult(Entity.Relation entity)
    {
        return mapper.MapRelationToRelationResult(entity);
    }

    private Either<ErrorResult, Entity.Relation> FetchFromDatabase(
        GetRelationByIdScenarioContext context,
        CancellationToken cancellationToken)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            Entity.Relation entity = ctx.Relations.First(i => i.Id == context.Payload.Id);
            return entity;
        }
        catch (Exception e)
        {
            return new ErrorResult(_correlationId, e.Message);
        }
    }
}

public record GetRelationByIdScenarioContext(Guid CorrelationId, RelationInput Payload);