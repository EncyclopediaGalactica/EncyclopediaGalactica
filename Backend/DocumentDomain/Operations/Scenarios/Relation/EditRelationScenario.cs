namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;

using System.Text;
using EncyclopediaGalactica.Common.Contracts;
using FluentValidation.Results;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class EditRelationScenario(
    EditRelationScenarioInputValidator validator,
    RelationMapper mapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions
)
{
    private Guid _correlationId;

    public async Task<Either<ErrorResult, RelationResult>> ExecuteAsync(
        EditRelationScenarioContext context,
        CancellationToken cancellationToken = default
    )
    {
        _correlationId = context.CorrelationId;
        Either<ErrorResult, RelationResult> result =
            from validatedInput in ValidateInput(context.Payload)
            from mappedToEntity in MapToRelationEntity(validatedInput)
            from updatedEntity in UpdateDatabase(mappedToEntity)
            from mappedResult in MapToRelationResult(updatedEntity)
            select mappedResult;
        return result;
    }

    private Either<ErrorResult, RelationResult> MapToRelationResult(Entity.Relation entity)
    {
        return mapper.MapRelationToRelationResult(entity);
    }

    private Either<ErrorResult, Entity.Relation> UpdateDatabase(Entity.Relation entity)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            Entity.Relation target = ctx.Relations.First(i => i.Id == entity.Id);
            target.LeftId = entity.LeftId;
            target.RightId = entity.RightId;
            ctx.Entry(target).State = EntityState.Modified;
            ctx.SaveChanges();
            return target;
        }
        catch (Exception e)
        {
            return new ErrorResult(_correlationId, e.Message);
        }
    }

    private Either<ErrorResult, Entity.Relation> MapToRelationEntity(RelationInput input)
    {
        return mapper.MapRelationInputToRelation(input);
    }

    private Either<ErrorResult, RelationInput> ValidateInput(RelationInput input)
    {
        ValidationResult? result = validator.Validate(input);
        if (result.IsValid)
        {
            StringBuilder builder = new();
            result.Errors.ForEach(item =>
                builder
                    .Append("Property name: ")
                    .Append(item.PropertyName)
                    .Append(' ')
                    .Append("error message: ")
                    .Append(item.ErrorMessage)
            );
            return new ErrorResult(_correlationId, builder.ToString());
        }

        return input;
    }
}

public record EditRelationScenarioContext(Guid CorrelationId, RelationInput Payload);