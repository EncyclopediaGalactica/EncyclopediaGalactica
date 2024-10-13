namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using System.Collections.Immutable;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using Infrastructure.Database;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class GetRelationTypesScenario(
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Either<ErrorResult, ImmutableList<RelationTypeResult>>> ExecuteAsync(
        GetRelationTypesScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, ImmutableList<RelationTypeResult>> result =
            from resultList in ExecuteDatabaseOperation(context, cancellationToken)
            from mappedResult in MapToRelationTypesImmutableList(context.CorrelationId, resultList)
            select mappedResult;
        return result;
    }

    private Either<ErrorResult, ImmutableList<RelationTypeResult>> MapToRelationTypesImmutableList(
        Guid correlationId,
        List<RelationType> relationTypeList) =>
        relationTypeList.MapToRelationTypeResultsImmutableList();

    private Either<ErrorResult, List<RelationType>> ExecuteDatabaseOperation(
        GetRelationTypesScenarioContext context,
        CancellationToken cancellationToken)
    {
        try
        {
            using DocumentDomainDbContext ctx = new(dbContextOptions);
            {
                return Either<ErrorResult, List<RelationType>>.Right(
                    ctx.RelationTypes.ToList());
            }
        }
        catch (Exception e)
        {
            return Either<ErrorResult, List<RelationType>>.Left(
                new ErrorResult(context.CorrelationId, e.Message));
        }
    }
}

public record GetRelationTypesScenarioContext(Guid CorrelationId);