namespace EncyclopediaGalactica.Scenarios.Storage.Edge;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Repository.Edge;
using Spectre.Console;

public class AddEdgeScenario(
    StorageContext ctx,
    AddEdgeScenarioInputValidator validator,
    EdgeRepository edgeRepository
)
{
    public Either<EgError, Unit> Execute(AddEdgeScenarioInput input) =>
        from validatedInput in validator.IsValid(input)
        from mappedEntity in validatedInput.ToEdgeEntity()
        from recordedEntity in edgeRepository.Add(mappedEntity, ctx)
        from mappedResult in recordedEntity.ToAddEdgeScenarioResult()
        from _ in RenderOperationResult(mappedResult)
        select Unit.Default;

    private Either<EgError, Unit> RenderOperationResult(AddEdgeScenarioResult result)
    {
        Table table = new();
        table.AddColumn("Id");
        table.AddColumn("From Vertex Id");
        table.AddColumn("To Vertex Id");
        table.AddColumn("Edge Type Id");

        table.AddRow(
            result.Id.ToString(),
            result.FromVertexId.ToString(),
            result.ToVertexId.ToString(),
            result.EdgeTypeId.ToString()
        );
        AnsiConsole.Write(table);
        return Unit.Default;
    }
}