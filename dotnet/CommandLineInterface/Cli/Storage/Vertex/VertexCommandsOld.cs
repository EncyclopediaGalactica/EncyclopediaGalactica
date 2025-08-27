namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Vertex;

using System.CommandLine;

public class VertexCommandsOld
{
    public Command CreateCommand()
    {
        Command vertexCommand = new("vertex");

        Command addCommand = new("add");
        vertexCommand.Subcommands.Add(addCommand);

        Command deleteCommand = new("delete");
        vertexCommand.Subcommands.Add(deleteCommand);

        Command listCommand = new("list");
        vertexCommand.Subcommands.Add(listCommand);

        Command updateCommand = new("update");
        vertexCommand.Subcommands.Add(updateCommand);

        return vertexCommand;
    }
}