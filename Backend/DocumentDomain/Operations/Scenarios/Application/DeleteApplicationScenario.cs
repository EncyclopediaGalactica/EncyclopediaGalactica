namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;

using Commands.Application;

public class DeleteApplicationScenario(
    DeleteApplicationCommand deleteApplicationCommand
)
{
    public async Task ExecuteAsync(
        DeleteApplicationScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        await deleteApplicationCommand.ExecuteAsync(context, cancellationToken).ConfigureAwait(false);
    }
}