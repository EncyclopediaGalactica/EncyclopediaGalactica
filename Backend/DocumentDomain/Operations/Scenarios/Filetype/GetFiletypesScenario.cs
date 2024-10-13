namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using Entity;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class GetFiletypesScenario(DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Either<ErrorResult, List<FiletypeResult>>> ExecuteAsync(
        GetFiletypesScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        Either<ErrorResult, List<FiletypeResult>> result =
            from filetypes in FetchFromDatabase(context.CorrelationId, cancellationToken)
            from mappedResult in MapToFiletypeResultList(filetypes, context.CorrelationId)
            select mappedResult;
        return result;
    }

    private Either<ErrorResult, List<FiletypeResult>> MapToFiletypeResultList(List<Filetype> filetypes, Guid correlationId)
    {
        return filetypes.MapToFiletypeResultList();
    }

    private Either<ErrorResult, List<Filetype>> FetchFromDatabase(Guid correlationId, CancellationToken cancellationToken)
    {
        using DocumentDomainDbContext ctx = new(dbContextOptions);
        try
        {
            List<Filetype> result = ctx.Filetypes.ToList();
            return Either<ErrorResult, List<Filetype>>.Right(result);
        }
        catch (Exception e)
        {
            return Either<ErrorResult, List<Filetype>>.Left(new ErrorResult(correlationId, e.Message));
        }
    }
}

public record GetFiletypesScenarioContext(Guid CorrelationId);