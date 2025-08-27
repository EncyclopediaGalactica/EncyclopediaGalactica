namespace EncyclopediaGalactica.Scenarios.Storage.EdgeType;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Repository.EdgeType;

public class AddEdgeTypeScenario(
    StorageContext ctx,
    EdgeTypeRepository repository,
    AddEdgeTypeScenarioInputValidator validator
)
{
    public Either<EgError, EdgeTypeResult> Execute(AddEdgeTypeScenarioInput input) =>
        from validatedInput in validator.IsValid(input)
        from mappedInput in validatedInput.ToEntity()
        from recordedEntity in repository.Add(mappedInput, ctx)
        from toResult in recordedEntity.ToEdgeTypeResult()
        select toResult;
}