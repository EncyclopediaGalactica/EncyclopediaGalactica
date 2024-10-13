namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;

using System.Text;
using EncyclopediaGalactica.Common.Contracts;
using FluentValidation.Results;
using Infrastructure.Database;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class DeleteRelationScenario(
    DeleteRelationScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    private Guid _correlationId;

    public async Task<Either<ErrorResult, RelationResult>> ExecuteAsync(
        DeleteRelationScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        _correlationId = context.CorrelationId;
        Either<ErrorResult, RelationResult> result =
            from validatedInput in ValidateInput(context.Payload)
            from _ in DeleteFromDatabase(validatedInput, cancellationToken)
            from deletionResult in CreateResult()
            select deletionResult;
        return result;
    }

    private Either<ErrorResult, RelationResult> CreateResult()
    {
        return new RelationResult();
    }

    private Either<ErrorResult, RelationInput> DeleteFromDatabase(RelationInput input, CancellationToken cancellationToken)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            Entity.Relation target = ctx.Relations.First(t => t.Id == input.Id);
            ctx.Entry(target).State = EntityState.Deleted;
            ctx.SaveChanges();
            return input;
        }
        catch (Exception e)
        {
            return new ErrorResult(_correlationId, e.Message);
        }
    }

    private Either<ErrorResult, RelationInput> ValidateInput(RelationInput input)
    {
        ValidationResult? result = validator.Validate(input);
        if (!result.IsValid)
        {
            StringBuilder builder = new();
            result.Errors.ForEach(error => builder.Append($"Property name:")
                .Append(' ')
                .Append(error.PropertyName)
                .Append(' ')
                .Append("Error message:")
                .Append(' ')
                .Append(error.ErrorMessage));
            return new ErrorResult(_correlationId, builder.ToString());
        }

        return input;
    }
}

public record DeleteRelationScenarioContext(Guid CorrelationId, RelationInput Payload);