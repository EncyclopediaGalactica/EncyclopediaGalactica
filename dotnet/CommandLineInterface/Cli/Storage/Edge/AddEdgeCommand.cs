namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Edge;

using System.ComponentModel;
using Common;
using Scenarios.Storage.Edge;
using Spectre.Console;
using Spectre.Console.Cli;

public sealed class AddEdgeCommand(
    AddEdgeScenario addEdgeScenario
) : Command<AddEdgeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Either<EgError, AddEdgeScenarioResult> executionResult =
            from mappedInput in settings.ToAddEdgeScenarioInput()
            from res in addEdgeScenario.Execute(mappedInput)
            select res;
        return executionResult.Match(
            Right: yolo =>
            {
                Either<EgError, Unit> r = from res in RenderOperationResult(yolo)
                    select res;
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

    private Either<EgError, Unit> RenderOperationResult(AddEdgeScenarioResult result)
    {
        try
        {
            Console.WriteLine($"result: {result.Id}, {result.FromVertexId}, {result.ToVertexId}, {result.EdgeTypeId}");
            Table table = new();
            table.AddColumn("Id")
                .AddColumn("From Vertex Id")
                .AddColumn("To Vertex Id")
                .AddColumn("Edge Type Id");

            table.AddRow(
                result.Id.ToString(),
                result.FromVertexId.ToString(),
                result.ToVertexId.ToString(),
                result.EdgeTypeId.ToString()
            );
            AnsiConsole.Write(table);
            return Right(Unit.Default);
        }
        catch (Exception e)
        {
            string message = $"{nameof(AddEdgeScenario)}.{nameof(RenderOperationResult)}: {e.Message}";
            return Left(new EgError(message, e.StackTrace));
        }
    }

    public sealed class Settings : EdgeSettings
    {
        [CommandOption("--edge-type-id", true)]
        [Description("The edge type id")]
        [DefaultValue(0)]
        public int EdgeTypeId { get; set; }

        [CommandOption("--from-edge-id")]
        [Description("The from edge id")]
        [DefaultValue(0)]
        public int FromEdgeId { get; set; }

        [CommandOption("--to-edge-id")]
        [Description("The to edge id")]
        public int ToEdgeId { get; set; }
    }
}

public static class AddEdgeCommandExtensions
{
    public static Either<EgError, AddEdgeScenarioInput> ToAddEdgeScenarioInput(this AddEdgeCommand.Settings settings)
    {
        try
        {
            AddEdgeScenarioInput input = new(
                settings.FromEdgeId,
                settings.ToEdgeId,
                settings.EdgeTypeId
            );
            return Right(input);
        }
        catch (Exception e)
        {
            string message = $"{nameof(AddEdgeCommandExtensions)}.{nameof(ToAddEdgeScenarioInput)}: {e.Message}";
            return Left(new EgError(message, e.StackTrace));
        }
    }
}