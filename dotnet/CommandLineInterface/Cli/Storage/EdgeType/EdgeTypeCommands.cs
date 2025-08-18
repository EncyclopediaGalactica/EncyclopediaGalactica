namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.EdgeType;

using System.CommandLine;
using Common;
using Scenarios.Storage.EdgeType;

public class EdgeTypeCommands(
    AddEdgeTypeScenario addEdgeTypeScenario,
    GetAllEdgeTypesScenario getAllEdgeTypesScenario
)
{
    public Command CreateCommand()
    {
        Command edgeTypeCommand = new("edge-type", "CRUD operations on edge types.");

        edgeTypeCommand.Subcommands.Add(CreateAddCommand());
        edgeTypeCommand.Subcommands.Add(CreateDeleteCommand());
        edgeTypeCommand.Subcommands.Add(CreateListCommand());
        edgeTypeCommand.Subcommands.Add(CreateUpdateCommand());
        return edgeTypeCommand;
    }

    private static Command CreateUpdateCommand()
    {
        Command updateCommand = new("update", "Update properties of the given edge type.");
        Option<long> idOption = new("--id") { Description = "The Id of the edge type going to be updated.", };
        updateCommand.Options.Add(idOption);
        Option<string> nameOption = new("--name") { Description = "The new name of the given edge type.", };
        updateCommand.Options.Add(nameOption);
        Option<string> descriptionOption =
            new("--description") { Description = "The new description of the given edge type.", };
        updateCommand.Options.Add(descriptionOption);
        return updateCommand;
    }

    private Command CreateListCommand()
    {
        Command listCommand = new("list", "Listing edge types.");
        listCommand.SetAction(parserResult =>
            {
                Either<EgError, int> r = from executionResult in getAllEdgeTypesScenario.Execute()
                    select executionResult;
                return r.Match(
                    Right: _ => 0,
                    Left: nopes =>
                    {
                        // some error message printing comes here
                        return 1;
                    }
                );
            }
        );
        return listCommand;
    }

    private static Command CreateDeleteCommand()
    {
        Command deleteCommand = new("delete");
        Option<long> idOption = new("--id") { Description = "The id of the edge type going to be deleted.", };
        deleteCommand.Options.Add(idOption);
        return deleteCommand;
    }

    private Command CreateAddCommand()
    {
        Command addCommand = new(
            "add",
            "Add a new edge to the system."
        );
        Option<string> edgeNameOption = new("--name")
        {
            Description =
                "The name of the edge type. It has to be unique. Validation rule: not null, not empty, trimmed is longer than 3 characters.",
        };
        addCommand.Options.Add(edgeNameOption);
        Option<string> edgeDescOption = new("--desc") { Description = "The rationale why we have this edge type.", };
        addCommand.Options.Add(edgeDescOption);
        addCommand.SetAction(parseResult =>
            {
                Either<EgError, EdgeTypeResult> r = from inputWithName in GetNameParameterValueAndToNameProperty(
                        new AddEdgeTypeScenarioInput(),
                        parseResult
                    )
                    from inputWithNameAndDesc in GetDescParameterValueAndAddToDescProperty(
                        inputWithName,
                        parseResult
                    )
                    from executionResult in addEdgeTypeScenario.Execute(inputWithNameAndDesc)
                    select executionResult;

                return r.Match(
                    Right: _ => 0,
                    Left: _ => 1
                );
            }
        );

        return addCommand;
    }

    private Either<EgError, AddEdgeTypeScenarioInput> GetDescParameterValueAndAddToDescProperty(
        AddEdgeTypeScenarioInput input,
        ParseResult parseResult
    )
    {
        string? description = parseResult.GetValue<string>("--desc");
        if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
        {
            return Left(
                new EgError(
                    $"{nameof(AddEdgeTypeScenarioInput)}.{nameof(AddEdgeTypeScenarioInput.Description)} must have a value.)"
                )
            );
        }

        return Right(input with { Description = description, });
    }

    private Either<EgError, AddEdgeTypeScenarioInput> GetNameParameterValueAndToNameProperty(
        AddEdgeTypeScenarioInput input,
        ParseResult parseResult
    )
    {
        string? name = parseResult.GetValue<string>("--name");
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return Left(new EgError($"Name parameter value is null, empty or whitespace."));
        }

        return Right(input with { Name = name, });
    }
}