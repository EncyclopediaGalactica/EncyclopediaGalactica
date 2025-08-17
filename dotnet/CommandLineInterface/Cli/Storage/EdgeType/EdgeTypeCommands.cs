namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.EdgeType;

using System.CommandLine;

public class EdgeTypeCommands
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

    private static Command CreateListCommand()
    {
        Command listCommand = new("list", "Listing edge types.");
        return listCommand;
    }

    private static Command CreateDeleteCommand()
    {
        Command deleteCommand = new("delete");
        Option<long> idOption = new("--id") { Description = "The id of the edge type going to be deleted.", };
        deleteCommand.Options.Add(idOption);
        return deleteCommand;
    }

    private static Command CreateAddCommand()
    {
        Command addCommand = new(
            "add",
            "Add a new edge to the system"
        );
        Option<string> edgeNameOption = new("--name")
        {
            Description = "The name of the edge type. It has to be unique.",
        };
        addCommand.Options.Add(edgeNameOption);
        Option<string> edgeDescOption = new("--desc") { Description = "The rationale why we have this edge type.", };
        addCommand.Options.Add(edgeDescOption);
        return addCommand;
    }
}