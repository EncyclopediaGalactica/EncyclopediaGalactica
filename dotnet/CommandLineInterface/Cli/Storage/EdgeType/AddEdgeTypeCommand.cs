namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.EdgeType;

using System.ComponentModel;
using Common;
using Scenarios.Storage.EdgeType;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Adding new edge type.")]
public sealed class AddEdgeTypeCommand(
    AddEdgeTypeScenario addEdgeTypeScenario
) : Command<AddEdgeTypeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, EdgeTypeResult> r =
            from scenarioInput in settings.ToAddEdgeTypeScenarioInput()
            from executionResult in addEdgeTypeScenario.Execute(scenarioInput)
            select executionResult;

        return r.Match(
            Right: yolo =>
            {
                Either<EgError, Unit> renderResult = RenderResult(yolo);
                return renderResult.Match(
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

    private Either<EgError, Unit> RenderResult(EdgeTypeResult edgeTypeResult)
    {
        try
        {
            Table table = new();
            table.Caption = new TableTitle($"The following Edge Type has been recorded");
            table.AddColumn("Id").AddColumn("Name").AddColumn("Description");
            table.AddRow(edgeTypeResult.Id.ToString(), edgeTypeResult.Name, edgeTypeResult.Description);
            AnsiConsole.Write(table);
            return Right(Unit.Default);
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }

    public sealed class Settings : EdgeTypeSettings
    {
        [CommandOption("-n|--name", true)]
        [Description("The name of the edge type")]
        public string Name { get; set; }

        [CommandOption("-d|--description", true)]
        [Description("The description of the edge type")]
        public string Description { get; set; }
    }
}

public static class SettingsExtensions
{
    public static Either<EgError, AddEdgeTypeScenarioInput> ToAddEdgeTypeScenarioInput(
        this AddEdgeTypeCommand.Settings settings
    )
    {
        try
        {
            return Right(new AddEdgeTypeScenarioInput { Name = settings.Name, Description = settings.Description, });
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }
}