namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Commands;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;
using Microsoft.Extensions.Logging;

public class NewRelationSaga(
    IAddNewRelationCommand addNewRelationCommand,
    IGetRelationByIdCommand getRelationByIdCommand,
    ILogger<NewRelationSaga> logger) : IHaveInputAndResultSaga<RelationResult, NewRelationHavePayloadScenarioContext>
{
    public async Task<Option<RelationResult>> ExecuteAsync(NewRelationHavePayloadScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            long relationId = await addNewRelationCommand.AddNewRelationAsync(context.Payload, cancellationToken)
                .ConfigureAwait(false);
            return await getRelationByIdCommand.GetByIdAsync(relationId, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened while executing {nameof(NewRelationSaga)}.";
            throw new SagaException(m, e);
        }
    }
}