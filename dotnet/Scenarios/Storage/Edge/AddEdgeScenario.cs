namespace EncyclopediaGalactica.Scenarios.Storage.Edge;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Repository.Edge;

public class AddEdgeScenario(
    StorageContext ctx,
    AddEdgeScenarioInputValidator validator,
    EdgeRepository edgeRepository
)
{
    public Either<EgError, AddEdgeScenarioResult> Execute(AddEdgeScenarioInput input) =>
        from validatedInput in validator.IsValid(input)
        from mappedEntity in validatedInput.ToEdgeEntity()
        from recordedEntity in edgeRepository.Add(mappedEntity, ctx)
        from mappedResult in recordedEntity.ToAddEdgeScenarioResult()
        select mappedResult;
}