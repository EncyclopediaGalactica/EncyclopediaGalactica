namespace EncyclopediaGalactica.BusinessLogic.Sagas.Relation;

using Commands.Relation;
using Contracts;
using Interfaces;
using Microsoft.Extensions.Logging;

public class NewRelationSaga(
    IAddNewRelationCommand addNewRelationCommand,
    IGetRelationByIdCommand getRelationByIdCommand,
    ILogger<NewRelationSaga> logger) : IHaveInputAndResultSaga<RelationResult, NewRelationSagaContext>
{
    public async Task<RelationResult> ExecuteAsync(NewRelationSagaContext context,
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