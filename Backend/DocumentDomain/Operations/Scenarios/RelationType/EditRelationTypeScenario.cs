namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using Common.Validation;
using DocumentDomain.Entity;
using EncyclopediaGalactica.Common.Contracts;
using FluentValidation.Results;
using Infrastructure.Database;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class EditRelationTypeScenario(
    EditRelationTypeScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions
)
{
    public async Task<Either<ErrorResult, RelationTypeResult>> ExecuteAsync(
        EditRelationTypeScenarioContext context,
        CancellationToken cancellationToken = default
    )
    {
        Either<ErrorResult, RelationTypeResult> result =
            from validatedInput in ValidateInput(context)
            from mappedInput in MapInputToEntity(context)
            from editedEntity in SaveToDatabase(
                mappedInput,
                context.CorrelationId,
                cancellationToken
            )
            from mappedEditedEntity in MapToRelationTypeResult(editedEntity, context.CorrelationId)
            select mappedEditedEntity;
        return result;
    }

    private Either<ErrorResult, RelationTypeResult> MapToRelationTypeResult(
        RelationType relationType,
        Guid correlationId
    )
    {
        return Either<ErrorResult, RelationTypeResult>.Right(
            relationType.MapToRelationTypeResult()
        );
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
            RelationType target = ctx.RelationTypes.First(w => w.Id == relationType.Id);
            target.Name = relationType.Name;
            target.Description = relationType.Description;
            ctx.Entry(target).State = EntityState.Modified;
            ctx.SaveChanges();
            return Either<ErrorResult, RelationType>.Right(target);
        }
        catch (Exception e)
        {
            return Either<ErrorResult, RelationType>.Left(
                new ErrorResult(correlationId, e.Message)
            );
        }
    }

    private static Either<ErrorResult, RelationType> MapInputToEntity(
        EditRelationTypeScenarioContext context
    )
    {
        return Either<ErrorResult, RelationType>.Right(context.Payload.MapToRelationType());
    }

    private Either<ErrorResult, RelationTypeInput> ValidateInput(
        EditRelationTypeScenarioContext context
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

public record EditRelationTypeScenarioContext(Guid CorrelationId, RelationTypeInput Payload);