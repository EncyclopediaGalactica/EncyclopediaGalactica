namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.EdgeType;

using System.ComponentModel;
using Common;
using Scenarios.Storage.EdgeType;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Listing edge types.")]
public sealed class ListEdgeTypeCommand(
    GetAllEdgeTypesScenario getAllEdgeTypesScenario
) : Command<ListEdgeTypeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, List<EdgeTypeResult>> result = from edgeTypes in getAllEdgeTypesScenario.Execute()
            select edgeTypes;
        return result.Match(
            Right: yolo =>
            {
                Either<EgError, Unit> r = from render in RenderResult(yolo)
                    select render;
                return r.Match(
                    Right: _ => 0,
                    Left: nopes =>
                    {
                        EgCli.RenderError(nopes);
                        return 1;
                    }
                );
            },
            Left: nopes =>
            {
                EgCli.RenderError(nopes);
                return 1;
            }
        );
    }

    private Either<EgError, Unit> RenderResult(List<EdgeTypeResult> edgeTypes)
    {
        Table table = new();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Description");

        edgeTypes.ForEach(item => table.AddRow(item.Id.ToString(), item.Name, item.Description));
        AnsiConsole.Write(table);
        return Right(Unit.Default);
    }

    public sealed class Settings : EdgeTypeSettings
    {
    }
}