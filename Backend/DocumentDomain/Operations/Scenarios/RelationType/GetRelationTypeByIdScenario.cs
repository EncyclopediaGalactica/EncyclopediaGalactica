namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using Common.Validation;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation.Results;
using Infrastructure.Database;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class GetRelationTypeByIdScenario(
    GetRelationTypeByIdScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Either<ErrorResult, RelationTypeResult>> ExecuteAsync(
        GetRelationTypeByIdScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, RelationTypeResult> result =
            from validatedInput in ValidateInput(context)
            from res in ExecuteDatabaseOperation(context, cancellationToken)
            from mappedResult in MapToRelationTypeResult(res, context)
            select mappedResult;
        return result;
    }

    private Either<ErrorResult, RelationTypeResult> MapToRelationTypeResult(
        RelationType result,
        GetRelationTypeByIdScenarioContext context) => result.MapToRelationTypeResult();

    private Either<ErrorResult, RelationType> ExecuteDatabaseOperation(
        GetRelationTypeByIdScenarioContext context,
        CancellationToken cancellationToken)
    {
        try
        {
            using DocumentDomainDbContext ctx = new(dbContextOptions);
            {
                RelationType target = ctx.RelationTypes.First(w => w.Id == context.Payload.Id);
                return Either<ErrorResult, RelationType>.Right(target);
            }
        }
        catch (Exception e)
        {
            return Either<ErrorResult, RelationType>.Left(
                new ErrorResult(context.CorrelationId, e.Message));
        }
    }

    private Either<ErrorResult, RelationTypeInput> ValidateInput(GetRelationTypeByIdScenarioContext context)
    {
        ValidationResult validationResult = validator.Validate(context.Payload);
        if (validationResult.IsValid)
        {
            return Either<ErrorResult, RelationTypeInput>.Right(context.Payload);
        }

        return Either<ErrorResult, RelationTypeInput>.Left(
            new ErrorResult(context.CorrelationId, validationResult.Errors.ToSummarize()));
    }
}

public record GetRelationTypeByIdScenarioContext(Guid CorrelationId, RelationTypeInput Payload);