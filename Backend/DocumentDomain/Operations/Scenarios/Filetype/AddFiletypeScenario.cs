namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using System.Text;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation.Results;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class AddFiletypeScenario(
    AddFiletypeScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions
)
{
    public async Task<Either<ErrorResult, FiletypeResult>> ExecuteAsync(
        AddFiletypeScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, FiletypeResult> result =
            from validatedInput in ValidateInput(context.Payload, context.CorrelationId)
            from mappedToEntity in MapToFiletypeEntity(validatedInput, context.CorrelationId)
            from savedEntity in SaveToDatabase(mappedToEntity, context.CorrelationId)
            from mappedSavedEntity in MapToFiletypeResult(savedEntity, context.CorrelationId)
            select mappedSavedEntity;
        return result;
    }

    private Either<ErrorResult, FiletypeResult> MapToFiletypeResult(Filetype filetype, Guid correlationId)
    {
        return Either<ErrorResult, FiletypeResult>.Right(filetype.MapToFiletypeResult());
    }

    private Either<ErrorResult, Filetype> SaveToDatabase(Filetype filetype, Guid correlationId)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            ctx.Filetypes.Add(filetype);
            ctx.SaveChanges();
            return Either<ErrorResult, Filetype>.Right(filetype);
        }
        catch (Exception e)
        {
            return Either<ErrorResult, Filetype>.Left(new ErrorResult(correlationId, e.Message));
        }
    }

    private Either<ErrorResult, Filetype> MapToFiletypeEntity(FiletypeInput payload, Guid correlationId)
    {
        return Either<ErrorResult, Filetype>.Right(payload.MapToFiletypeEntity());
    }

    private Either<ErrorResult, FiletypeInput> ValidateInput(FiletypeInput payload, Guid correlationId)
    {
        ValidationResult? result = validator.Validate(payload);
        if (result.IsValid)
        {
            return Either<ErrorResult, FiletypeInput>.Right(payload);
        }

        StringBuilder builder = new();
        result.Errors.ForEach(e => builder.Append($"Property name:")
            .Append(' ')
            .Append(e.PropertyName)
            .Append(' ')
            .Append("error:")
            .Append(' ')
            .Append(e.ErrorMessage)
        );
        return Either<ErrorResult, FiletypeInput>.Left(new ErrorResult(correlationId, builder.ToString()));
    }
}

public record AddFiletypeScenarioContext(Guid CorrelationId, FiletypeInput Payload);