namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using Common.Validation;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation.Results;
using Infrastructure.Database;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class DeleteRelationTypeScenario(
    DeleteRelationTypeScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Either<ErrorResult, RelationTypeResult>> ExecuteAsync(
        DeleteRelationTypeScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, RelationTypeResult> result =
            from validatedInput in ValidateInput(context)
            from dbOperationResult in ExecuteDatabaseOperation(context, cancellationToken)
            select dbOperationResult;
        return result;
    }

    private Either<ErrorResult, RelationTypeResult> ExecuteDatabaseOperation(
       DeleteRelationTypeScenarioContext context,
        CancellationToken cancellationToken)
    {
        try
        {
            using DocumentDomainDbContext ctx = new(dbContextOptions);
            {
                RelationType target = ctx.RelationTypes.First(w => w.Id == context.Payload.Id);
                ctx.Entry(target).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Either<ErrorResult, RelationTypeResult>.Right(new RelationTypeResult());
        }
        catch (Exception e)
        {
            return Either<ErrorResult, RelationTypeResult>.Left(new ErrorResult(context.CorrelationId, e.Message));
        }
    }

    private Either<ErrorResult, RelationTypeInput> ValidateInput(DeleteRelationTypeScenarioContext context)
    {
        ValidationResult validationResult = validator.Validate(context.Payload);
        if (validationResult.IsValid)
        {
            return Either<ErrorResult, RelationTypeInput>.Right(context.Payload);
        }

        return Either<ErrorResult, RelationTypeInput>.Left(new ErrorResult(context.CorrelationId, validationResult.Errors.ToSummarize()));
    }
}

public record DeleteRelationTypeScenarioContext(Guid CorrelationId, RelationTypeInput Payload);