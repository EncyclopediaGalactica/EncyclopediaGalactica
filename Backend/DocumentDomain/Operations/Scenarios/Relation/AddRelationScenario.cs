namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;

using System.Text;
using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class AddRelationScenario(
    AddRelationScenarioInputValidator validator,
    RelationMapper mapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions
)
{
    private Guid _correlationId;

    public async Task<Either<ErrorResult, RelationResult>> ExecuteAsync(
        AddRelationScenarioContext addRelationScenarioContext,
        CancellationToken cancellationToken = default)
    {
        _correlationId = addRelationScenarioContext.CorrelationId;

        Either<ErrorResult, RelationResult> result =
            from validatedInput in InputValidation(addRelationScenarioContext.Payload)
            from mappedToEntity in MapToRelation(validatedInput)
            from savedEntity in SaveToDatabase(mappedToEntity, cancellationToken)
            from mappedSavedEntity in MapToRelationResult(savedEntity)
            select mappedSavedEntity;
        return result;
    }

    private Either<ErrorResult, RelationResult> MapToRelationResult(Entity.Relation entity)
    {
        return Either<ErrorResult, RelationResult>.Right(mapper.MapRelationToRelationResult(entity));
    }

    private Either<ErrorResult, Entity.Relation> SaveToDatabase(
        Entity.Relation relation,
        CancellationToken cancellationToken)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            EntityEntry<Entity.Relation> res = ctx.Relations.Add(relation);
            ctx.SaveChanges();
            return Either<ErrorResult, Entity.Relation>.Right(res.Entity);
        }
        catch (Exception e)
        {
            return new ErrorResult(_correlationId, e.Message);
        }
    }

    private Either<ErrorResult, Entity.Relation> MapToRelation(RelationInput relationInput)
    {
        return Either<ErrorResult, Entity.Relation>.Right(mapper.MapRelationInputToRelation(relationInput));
    }

    private Either<ErrorResult, RelationInput> InputValidation(RelationInput input)
    {
        ValidationResult? validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
        {
            StringBuilder builder = new();
            validationResult.Errors.ForEach(error => builder.Append($"Error at {error.PropertyName} with error message: {error.ErrorMessage}."));
            return Either<ErrorResult, RelationInput>.Left(new ErrorResult(_correlationId, builder.ToString()));
        }

        return Either<ErrorResult, RelationInput>.Right(input);
    }
}

public record AddRelationScenarioContext(
    Guid CorrelationId,
    RelationInput Payload);