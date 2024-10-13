namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using Common.Validation;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation.Results;
using Infrastructure.Database;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class AddRelationTypeScenario(
    AddRelationTypeScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions
)
{
    public async Task<Either<ErrorResult, RelationTypeResult>> ExecuteAsync(
        AddRelationTypeScenarioContext context,
        CancellationToken cancellationToken = default
    )
    {
        Either<ErrorResult, RelationTypeResult> result =
            from validatedInput in ValidateInput(context)
            from mappedInput in MapToRelationType(context)
            from savedResult in SaveToDatabase(
                mappedInput,
                context.CorrelationId,
                cancellationToken
            )
            from mappedSavedResult in MapToRelationTypeResult(savedResult, context.CorrelationId)
            select mappedSavedResult;
        return result;
    }

    private Either<ErrorResult, RelationTypeResult> MapToRelationTypeResult(
        RelationType savedResult,
        Guid correlationId
    )
    {
        return Either<ErrorResult, RelationTypeResult>.Right(savedResult.MapToRelationTypeResult());
    }

    private Either<ErrorResult, RelationType> SaveToDatabase(
        RelationType relationType,
        Guid correlationId,
        CancellationToken cancellationToken
    )
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            EntityEntry<RelationType> savedResult = ctx.RelationTypes.Add(relationType);
            ctx.SaveChanges();
            return Either<ErrorResult, RelationType>.Right(savedResult.Entity);
        }
        catch (Exception e)
        {
            return Either<ErrorResult, RelationType>.Left(
                new ErrorResult(correlationId, e.Message)
            );
        }
    }

    private Either<ErrorResult, RelationType> MapToRelationType(
        AddRelationTypeScenarioContext context
    )
    {
        return Either<ErrorResult, RelationType>.Right(context.Payload.MapToRelationType());
    }

    private Either<ErrorResult, RelationTypeInput> ValidateInput(
        AddRelationTypeScenarioContext context
    )
    {
        ValidationResult validationResult = validator.Validate(context.Payload);
        if (validationResult.IsValid)
        {
            return Either<ErrorResult, RelationTypeInput>.Right(context.Payload);
        }

        return Either<ErrorResult, RelationTypeInput>.Left(
            new ErrorResult(context.CorrelationId, validationResult.Errors.ToSummarize())
        );
    }
}

public record AddRelationTypeScenarioContext(Guid CorrelationId, RelationTypeInput Payload);