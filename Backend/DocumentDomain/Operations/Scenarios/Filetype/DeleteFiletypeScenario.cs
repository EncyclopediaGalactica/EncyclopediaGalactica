namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using System.Text;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation.Results;
using Infrastructure.Database;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class DeleteFiletypeScenario(
    DeleteFiletypeScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Either<ErrorResult, FiletypeResult>> ExecuteAsync(
        DeleteFiletypeScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, FiletypeResult> result =
            from validatedInput in ValidateInput(context.Payload, context.CorrelationId)
            from _ in DeleteFromDatabase(validatedInput, context.CorrelationId, cancellationToken)
            select new FiletypeResult();
        return result;
    }

    private Either<ErrorResult, FiletypeResult> DeleteFromDatabase(FiletypeInput input, Guid correlationId, CancellationToken cancellationToken)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            Filetype target = ctx.Filetypes.First(w => w.Id == input.Id);
            ctx.Entry(target).State = EntityState.Deleted;
            ctx.SaveChanges();
            return Either<ErrorResult, FiletypeResult>.Right(new FiletypeResult());
        }
        catch (Exception e)
        {
            return Either<ErrorResult, FiletypeResult>.Left(new ErrorResult(correlationId, e.Message));
        }
    }

    private Either<ErrorResult, FiletypeInput> ValidateInput(FiletypeInput input, Guid correlationId)
    {
        ValidationResult validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            return Either<ErrorResult, FiletypeInput>.Right(input);
        }

        StringBuilder builder = new();
        validationResult.Errors.ForEach(e => builder.Append("Property name:")
            .Append(' ')
            .Append(e.PropertyName)
            .Append(' ')
            .Append("Error message:")
            .Append(' ')
            .Append(e.ErrorMessage));
        return Either<ErrorResult, FiletypeInput>.Left(new ErrorResult(correlationId, builder.ToString()));
    }
}

public record DeleteFiletypeScenarioContext(Guid CorrelationId, FiletypeInput Payload);