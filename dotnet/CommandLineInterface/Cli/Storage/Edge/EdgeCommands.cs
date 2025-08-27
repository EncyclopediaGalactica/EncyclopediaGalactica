// namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Edge;
//
// using System.CommandLine;
// using Common;
// using Scenarios.Storage.Edge;
//
// public class EdgeCommands(
//     AddEdgeScenario addEdgeScenario,
//     GetAllEdgesScenario getAllEdgesScenario
// )
// {
//     public Command CreateCommand()
//     {
//         Command edgeCommand = new("edge");
//         Either<EgError, Command> result = from createdAddCommand in CreateAddCommand(edgeCommand)
//             from createdDeleteCommand in CreateDeleteCommand(createdAddCommand)
//             from createdListCommand in CreateListCommand(createdDeleteCommand)
//             from createdUpdateCommand in CreateUpdateCommand(createdListCommand)
//             select createdUpdateCommand;
//
//         return result.Match(
//             Right: yolo => yolo,
//             Left: nopes =>
//             {
//                 Console.WriteLine($"Nopes at creating edge command: {nopes.Message}");
//                 throw new InvalidOperationException("Invalid command");
//             }
//         );
//     }
//
//     private Either<EgError, Command> CreateAddCommand(Command command)
//     {
//         Command addCommand = new("add");
//         Option<string> edgeTypeId = new("--edge-type-id")
//         {
//             Description = "The type reference of the type of this command.", Required = true,
//         };
//         addCommand.Options.Add(edgeTypeId);
//         Option<long> fromVertexOption =
//             new("--from-vertex-id") { Description = "The from-vertex ID.", Required = false, };
//         addCommand.Options.Add(fromVertexOption);
//         Option<long> toVertexOption = new("--to-vertex-id") { Description = "The to-vertex ID.", Required = false, };
//         addCommand.Options.Add(toVertexOption);
//         addCommand.SetAction(parseResult =>
//             {
//                 AddEdgeScenarioInput input = new();
//                 Either<EgError, Unit> executionResult =
//                     from edgeTypeAdded in ExtractEdgeTypeIdAndAddToDto(
//                         input,
//                         parseResult
//                     )
//                     from fromEdgeId in ExtractFromVertexIdAndAddToDto(edgeTypeAdded, parseResult)
//                     from toEdgeId in ExtractToVertexIdAndAddToDto(fromEdgeId, parseResult)
//                     from res in addEdgeScenario.Execute(toEdgeId)
//                     select res;
//                 return executionResult.Match(
//                     Right: _ => 0,
//                     Left: nopes =>
//                     {
//                         Console.WriteLine($"Message: {nopes.Message} \n Trace: {nopes.Trace}");
//                         return 1;
//                     }
//                 );
//             }
//         );
//         command.Subcommands.Add(addCommand);
//         return Right(command);
//     }
//
//     private Either<EgError, AddEdgeScenarioInput> ExtractToVertexIdAndAddToDto(
//         AddEdgeScenarioInput input,
//         ParseResult parseResult
//     )
//         => from parsedResult in LanguageExtHelpers.GetValueFromParseResult<long>("--to-vertex-id", parseResult)
//             select input with { ToVertexId = parsedResult, };
//
//     private Either<EgError, AddEdgeScenarioInput> ExtractFromVertexIdAndAddToDto(
//         AddEdgeScenarioInput input,
//         ParseResult parseResult
//     ) =>
//         from parsedValue in LanguageExtHelpers.GetValueFromParseResult<long>("--from-vertex-id", parseResult)
//         select input with { FromVertexId = parsedValue, };
//
//     private Either<EgError, AddEdgeScenarioInput> ExtractEdgeTypeIdAndAddToDto(
//         AddEdgeScenarioInput input,
//         ParseResult parseResult
//     ) =>
//         from parsedValue in LanguageExtHelpers.GetValueFromParseResult<long>("--edge-type-id", parseResult)
//         select input with { EdgeTypeId = parsedValue, };
//
//     private Either<EgError, Command> CreateUpdateCommand(Command command)
//     {
//         Command updateCommand = new("update");
//         command.Subcommands.Add(updateCommand);
//         return Right(command);
//     }
//
//     private Either<EgError, Command> CreateListCommand(Command command)
//     {
//         Command listCommand = new("list");
//         command.Subcommands.Add(listCommand);
//         command.SetAction(_ =>
//             {
//                 Either<EgError, Unit> executionResult = getAllEdgesScenario.Execute();
//                 return executionResult.Match(
//                     Right: _ => 0,
//                     Left: nopes =>
//                     {
//                         Console.WriteLine($"{nopes.Message}");
//                         return 1;
//                     }
//                 );
//             }
//         );
//         return Right(command);
//     }
//
//     private Either<EgError, Command> CreateDeleteCommand(Command command)
//     {
//         Command deleteCommand = new("delete");
//         command.Subcommands.Add(deleteCommand);
//         return Right(command);
//     }
// }

