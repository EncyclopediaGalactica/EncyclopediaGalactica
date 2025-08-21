namespace EncyclopediaGalactica.Scenarios.Storage.EdgeType;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Entities;
using EncyclopediaGalactica.Storage.Repository.EdgeType;
using Spectre.Console;

public class GetAllEdgeTypesScenario(
    EdgeTypeRepository repository,
    StorageContext ctx
)
{
    public Either<EgError, int> Execute()
        => from edgeTypes in repository.GetAll(ctx)
            from renderResult in Render(edgeTypes)
            select 0;

    private Either<EgError, int> Render(List<EdgeTypeEntity> edgeTypes)
    {
        Table table = new();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("Reference");

        edgeTypes.ForEach(item => table.AddRow(item.Id.ToString(), item.Name, item.Description, item.Reference));
        AnsiConsole.Write(table);
        return Right(0);
    }
}