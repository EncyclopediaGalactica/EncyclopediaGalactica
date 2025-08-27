namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Edge;

using System.ComponentModel;
using Spectre.Console.Cli;

public sealed class UpdateEdgeCommand : Command<UpdateEdgeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        //             AddEdgeScenarioInput input = new();
        //             Either<EgError, Unit> executionResult =
        //                 from edgeTypeAdded in ExtractEdgeTypeIdAndAddToDto(
        //                     input,
        //                     parseResult
        //                 )
        //                 from fromEdgeId in ExtractFromVertexIdAndAddToDto(edgeTypeAdded, parseResult)
        //                 from toEdgeId in ExtractToVertexIdAndAddToDto(fromEdgeId, parseResult)
        //                 from res in addEdgeScenario.Execute(toEdgeId)
        //                 select res;
        //             return executionResult.Match(
        //                 Right: _ => 0,
        //                 Left: nopes =>
        //                 {
        //                     Console.WriteLine($"Message: {nopes.Message} \n Trace: {nopes.Trace}");
        //                     return 1;
        //                 }
        //             );
        Console.WriteLine("Adding edge type");
        return 0;
    }

    public sealed class Settings : EdgeSettings
    {
        [CommandOption("--edge-type-id", true)]
        [Description("The edge type id")]
        public long EdgeTypeId { get; set; }

        [CommandOption("--from-edge-id")]
        [Description("The from edge id")]
        public long FromEdgeId { get; set; }

        [CommandOption("--to-edge-id")]
        [Description("The to edge id")]
        public long ToEdgeId { get; set; }
    }

    // private Either<EgError, System.CommandLine.Command> CreateAddCommand(System.CommandLine.Command command)
    // {
    //     System.CommandLine.Command addCommand = new("add");
    //     Option<string> edgeTypeId = new("--edge-type-id")
    //     {
    //         Description = "The type reference of the type of this command.", Required = true,
    //     };
    //     addCommand.Options.Add(edgeTypeId);
    //     Option<long> fromVertexOption =
    //         new("--from-vertex-id") { Description = "The from-vertex ID.", Required = false, };
    //     addCommand.Options.Add(fromVertexOption);
    //     Option<long> toVertexOption = new("--to-vertex-id") { Description = "The to-vertex ID.", Required = false, };
    //     addCommand.Options.Add(toVertexOption);
    //     addCommand.SetAction(parseResult =>
    //         {
    //             AddEdgeScenarioInput input = new();
    //             Either<EgError, Unit> executionResult =
    //                 from edgeTypeAdded in ExtractEdgeTypeIdAndAddToDto(
    //                     input,
    //                     parseResult
    //                 )
    //                 from fromEdgeId in ExtractFromVertexIdAndAddToDto(edgeTypeAdded, parseResult)
    //                 from toEdgeId in ExtractToVertexIdAndAddToDto(fromEdgeId, parseResult)
    //                 from res in addEdgeScenario.Execute(toEdgeId)
    //                 select res;
    //             return executionResult.Match(
    //                 Right: _ => 0,
    //                 Left: nopes =>
    //                 {
    //                     Console.WriteLine($"Message: {nopes.Message} \n Trace: {nopes.Trace}");
    //                     return 1;
    //                 }
    //             );
    //         }
    //     );
    //     command.Subcommands.Add(addCommand);
    //     return Right(command);
    // }
    //
    // private Either<EgError, AddEdgeScenarioInput> ExtractToVertexIdAndAddToDto(
    //     AddEdgeScenarioInput input,
    //     ParseResult parseResult
    // )
    //     => from parsedResult in LanguageExtHelpers.GetValueFromParseResult<long>("--to-vertex-id", parseResult)
    //         select input with { ToVertexId = parsedResult, };
    //
    // private Either<EgError, AddEdgeScenarioInput> ExtractFromVertexIdAndAddToDto(
    //     AddEdgeScenarioInput input,
    //     ParseResult parseResult
    // ) =>
    //     from parsedValue in LanguageExtHelpers.GetValueFromParseResult<long>("--from-vertex-id", parseResult)
    //     select input with { FromVertexId = parsedValue, };
    //
    // private Either<EgError, AddEdgeScenarioInput> ExtractEdgeTypeIdAndAddToDto(
    //     AddEdgeScenarioInput input,
    //     ParseResult parseResult
    // ) =>
    //     from parsedValue in LanguageExtHelpers.GetValueFromParseResult<long>("--edge-type-id", parseResult)
    //     select input with { EdgeTypeId = parsedValue, };
    //
    // private Either<EgError, System.CommandLine.Command> CreateUpdateCommand(System.CommandLine.Command command)
    // {
    //     System.CommandLine.Command updateCommand = new("update");
    //     command.Subcommands.Add(updateCommand);
    //     return Right(command);
    // }
    //
    // private Either<EgError, System.CommandLine.Command> CreateListCommand(System.CommandLine.Command command)
    // {
    //     System.CommandLine.Command listCommand = new("list");
    //     command.Subcommands.Add(listCommand);
    //     command.SetAction(_ =>
    //         {
    //             Either<EgError, Unit> executionResult = getAllEdgesScenario.Execute();
    //             return executionResult.Match(
    //                 Right: _ => 0,
    //                 Left: nopes =>
    //                 {
    //                     Console.WriteLine($"{nopes.Message}");
    //                     return 1;
    //                 }
    //             );
    //         }
    //     );
    //     return Right(command);
    // }
    //
    // private Either<EgError, System.CommandLine.Command> CreateDeleteCommand(System.CommandLine.Command command)
    // {
    //     System.CommandLine.Command deleteCommand = new("delete");
    //     command.Subcommands.Add(deleteCommand);
    //     return Right(command);
    // }
}