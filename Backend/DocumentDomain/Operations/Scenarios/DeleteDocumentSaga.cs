namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Commands;
using Common.Scenario;
using Microsoft.Extensions.Logging;

public class DeleteDocumentSaga(
    IDeleteDocumentCommand deleteDocumentCommand,
    IDeleteStructureNodesCommand deleteStructureNodesCommand,
    ILogger<DeleteDocumentSaga> logger) : IHaveInputSaga<DeleteDocumentHavePayloadScenarioContext>
{
    public async Task ExecuteAsync(DeleteDocumentHavePayloadScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await deleteDocumentCommand.DeleteAsync(context.Payload, cancellationToken)
                .ConfigureAwait(false);
            await deleteStructureNodesCommand.DeleteAsync(context.Payload, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during {nameof(DeleteDocumentSaga)}.";
            throw new SagaException(m, e);
        }
    }
}