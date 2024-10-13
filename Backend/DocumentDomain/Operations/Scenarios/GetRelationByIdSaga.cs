namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Commands;
using Common.Commands.Exceptions;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;
using Microsoft.Extensions.Logging;

public class GetRelationByIdSaga(
    IGetRelationByIdCommand getRelationByIdCommand,
    ILogger<GetRelationByIdSaga> logger)
    : IHaveInputAndResultSaga<RelationResult, GetRelationByIdHavePayloadScenarioContext>
{
    public async Task<Option<RelationResult>> ExecuteAsync(GetRelationByIdHavePayloadScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await SagaBusinessLogicAsync(context.Payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened while executing {nameof(GetRelationByIdSaga)} saga.";
            throw new SagaException(m, e);
        }
    }

    private async Task<RelationResult> SagaBusinessLogicAsync(long payload, CancellationToken cancellationToken)
    {
        ValidateInput(payload);
        return await getRelationByIdCommand.GetByIdAsync(payload, cancellationToken).ConfigureAwait(false);
    }

    private void ValidateInput(long payload)
    {
        long notAllowedValue = 0;
        if (payload == notAllowedValue)
        {
            string m = $"{nameof(payload)} cannot be {notAllowedValue}";
            throw new InvalidArgumentCommandException(m);
        }
    }
}