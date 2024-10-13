namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;

using Commands.Application;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;

public class AddApplicationScenario(IAddApplicationCommand addApplicationCommand)
{
    public async Task<Option<ApplicationResult>> ExecuteAsync(
        AddApplicationScenarioContext context,
        CancellationToken cancellationToken = default
    )
    {
        return await addApplicationCommand
            .ExecuteAsync(context, cancellationToken)
            .ConfigureAwait(false);
    }
}