namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands.Application;

using EncyclopediaGalactica.Common.Contracts;
using Entity;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Scenarios.Application;

public class GetApplicationsCommand(
    ApplicationMapper applicationMapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<List<ApplicationResult>> ExecuteAsync(
        GetApplicationsScenarioContext context,
        CancellationToken cancellationToken)
    {
        return await ExecuteOperationAsync(context, cancellationToken).ConfigureAwait(false);
    }

    private async Task<List<ApplicationResult>> ExecuteOperationAsync(GetApplicationsScenarioContext context, CancellationToken cancellationToken)
    {
        List<Application> applications = await ExecuteDatabaseOperation(context, cancellationToken)
            .ConfigureAwait(false);
        return applicationMapper.ToApplicationResults(applications);
    }

    private async Task<List<Application>> ExecuteDatabaseOperation(GetApplicationsScenarioContext context, CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        return await ctx.Applications.ToListAsync(cancellationToken);
    }
}