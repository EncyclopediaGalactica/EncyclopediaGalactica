namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using System.Text;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation.Results;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class EditFiletypeScenario(
    EditFiletypeScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Either<ErrorResult, FiletypeResult>> ExecuteAsync(
        EditFiletypeScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, FiletypeResult> result =
            from validatedInput in ValidateInput(context.Payload, context.CorrelationId)
            from mappedInput in MapToFiletype(validatedInput, context.CorrelationId)
            from savedEntity in SaveToDatabase(mappedInput, context.CorrelationId)
            from mappedResult in MapToFiletypeResult(savedEntity, context.CorrelationId)
            select mappedResult;
        return result;
    }

    private Either<ErrorResult, FiletypeResult> MapToFiletypeResult(Filetype filetype, Guid correlationId)
    {
        return filetype.MapToFiletypeResult();
    }

    private Either<ErrorResult, Filetype> SaveToDatabase(Filetype input, Guid correlationId)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            Filetype target = ctx.Filetypes.First(f => f.Id == input.Id);
            target.Name = input.Name;
            target.Description = input.Description;
            target.FileExtension = input.FileExtension;
            ctx.Entry(target).State = EntityState.Modified;
            ctx.SaveChanges();
            return Either<ErrorResult, Filetype>.Right(target);
        }
        catch (Exception e)
        {
            return Either<ErrorResult, Filetype>.Left(new ErrorResult(correlationId, e.Message));
        }
    }

    private Either<ErrorResult, Filetype> MapToFiletype(FiletypeInput input, Guid correlationId)
    {
        return Either<ErrorResult, Filetype>.Right(input.MapToFiletypeEntity());
    }

    private Either<ErrorResult, FiletypeInput> ValidateInput(FiletypeInput input, Guid correlationId)
    {
        ValidationResult result = validator.Validate(input);
        if (result.IsValid)
        {
            return Either<ErrorResult, FiletypeInput>.Right(input);
        }

        StringBuilder builder = new();
        result.Errors.ForEach(er => builder
            .Append("Property name")
            .Append(' ')
            .Append($"{er.PropertyName}")
            .Append(' ')
            .Append($"{er.ErrorMessage}"));
        return Either<ErrorResult, FiletypeInput>.Left(new ErrorResult(correlationId, builder.ToString()));
    }
}

public record EditFiletypeScenarioContext(Guid CorrelationId, FiletypeInput Payload);