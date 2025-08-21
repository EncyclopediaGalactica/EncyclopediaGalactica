namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage;

using Edge;
using EdgeType;
using Spectre.Console.Cli;
using Vertex;

public class StorageCommandOld(
    VertexCommandsOld vertexCommandsOld,
    EdgeCommands edgeCommands,
    EdgeTypeCommandsOld edgeTypeCommandsOld
)
{
    public System.CommandLine.Command CreateCommand()
    {
        System.CommandLine.Command storageCommand = new("storage");
        storageCommand.Subcommands.Add(vertexCommandsOld.CreateCommand());
        storageCommand.Subcommands.Add(edgeCommands.CreateCommand());
        storageCommand.Subcommands.Add(edgeTypeCommandsOld.CreateCommand());
        return storageCommand;
    }
}

public class StorageSettings : CommandSettings
{
}