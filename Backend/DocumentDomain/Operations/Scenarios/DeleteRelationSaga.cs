#region

#endregion

namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Commands;
using Common.Commands.Exceptions;
using Common.Scenario;
using Microsoft.Extensions.Logging;

public class DeleteRelationSaga(
    IDeleteRelationCommand deleteRelationCommand,
    ILogger<DeleteRelationSaga> logger) : IHaveInputSaga<DeleteRelationHavePayloadScenarioContext>
{
    public async Task ExecuteAsync(DeleteRelationHavePayloadScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ExecuteBusinessLogicAsync(context.Payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during execution of {nameof(DeleteRelationSaga)} saga.";
            throw new SagaException(m, e);
        }
    }

    private async Task ExecuteBusinessLogicAsync(long contextPayload, CancellationToken cancellationToken)
    {
        ValidateInput(contextPayload);
        await deleteRelationCommand.DeleteAsync(contextPayload, cancellationToken).ConfigureAwait(false);
    }

    private void ValidateInput(long relationId)
    {
        long notAllowedValue = 0;
        if (relationId == notAllowedValue)
        {
            string m = $"{nameof(relationId)} cannot be {notAllowedValue}";
            throw new InvalidArgumentCommandException(m);
        }
    }
}