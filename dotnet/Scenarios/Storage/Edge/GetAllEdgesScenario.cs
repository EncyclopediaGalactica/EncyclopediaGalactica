namespace EncyclopediaGalactica.Scenarios.Storage.Edge;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Entities;
using EncyclopediaGalactica.Storage.Repository.Edge;
using Spectre.Console;

public class GetAllEdgesScenario(
    StorageContext ctx,
    EdgeRepository edgeRepository
)
{
    public Either<EgError, Unit> Execute() =>
        from edges in edgeRepository.GetAll(ctx)
        from _ in RenderResult(edges)
        select unit;

    private static Either<EgError, Unit> RenderResult(List<EdgeEntity> edges)
    {
        try
        {
            Table table = new();
            table.AddColumn("Id")
                .AddColumn("From Vertex Id")
                .AddColumn("To Vertex Id");

            edges.ForEach(item =>
                {
                    table.AddRow(item.Id.ToString(), item.FromVertexId.ToString(), item.ToVertexId.ToString());
                }
            );
            AnsiConsole.Write(table);
            return Right(Unit.Default);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}