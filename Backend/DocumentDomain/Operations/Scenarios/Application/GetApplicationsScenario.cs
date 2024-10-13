namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;

using Commands.Application;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;

public class GetApplicationsScenario(GetApplicationsCommand getApplicationsCommand)
{
    public async Task<Option<List<ApplicationResult>>> ExecuteAsync(
        GetApplicationsScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        return await getApplicationsCommand.ExecuteAsync(
            context,
            cancellationToken).ConfigureAwait(false);
    }
}