namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using System.Text;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation.Results;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class GetFiletypeByIdScenario(
    GetFiletypeByIdScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions
)
{
    public async Task<Either<ErrorResult, FiletypeResult>> ExecuteAsync(
        GetFiletypeByIdScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, FiletypeResult> result =
            from validatedInput in ValidateInput(context.Payload, context.CorrelationId)
            from foundEntity in FindInDatabase(validatedInput, context.CorrelationId)
            from mappedResult in MapToFiletypeResult(foundEntity, context.CorrelationId)
            select mappedResult;
        return result;
    }

    private Either<ErrorResult, FiletypeResult> MapToFiletypeResult(Filetype filetype, Guid correlationId)
    {
        return filetype.MapToFiletypeResult();
    }

    private Either<ErrorResult, Filetype> FindInDatabase(FiletypeInput input, Guid correlationId)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            Filetype target = ctx.Filetypes.First(w => w.Id == input.Id);
            return Either<ErrorResult, Filetype>.Right(target);
        }
        catch (Exception e)
        {
            return Either<ErrorResult, Filetype>.Left(new ErrorResult(correlationId, e.Message));
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
        validationResult.Errors.ForEach(err => builder.Append("Property name")
            .Append(' ')
            .Append(err.PropertyName)
            .Append(' ')
            .Append("message:")
            .Append(' ')
            .Append(err.ErrorMessage));
        return Either<ErrorResult, FiletypeInput>.Left(new ErrorResult(correlationId, builder.ToString()));
    }
}

public record GetFiletypeByIdScenarioContext(Guid CorrelationId, FiletypeInput Payload);