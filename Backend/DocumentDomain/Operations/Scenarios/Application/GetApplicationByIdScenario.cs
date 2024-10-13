namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;

using Commands.Application;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;

public class GetApplicationByIdScenario(GetApplicationByIdCommand getApplicationByIdCommand)
{
    public async Task<Option<ApplicationResult>> ExecuteAsync(
        GetApplicationByIdScenarioContext ctx,
        CancellationToken cancellationToken = default)
    {
        return await getApplicationByIdCommand.ExecuteAsync(
            new GetApplicationByIdScenarioContext
            {
                CorrelationId = Guid.NewGuid(),
                Payload = ctx.Payload
            }, cancellationToken).ConfigureAwait(false);
    }
}