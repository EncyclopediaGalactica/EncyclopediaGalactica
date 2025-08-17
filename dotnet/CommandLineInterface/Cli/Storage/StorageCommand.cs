namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage;

using System.CommandLine;
using Edge;
using EdgeType;
using Vertex;

public class StorageCommand(
    VertexCommands vertexCommands,
    EdgeCommands edgeCommands,
    EdgeTypeCommands edgeTypeCommands
    )
{
    public Command CreateCommand()
    {
        Command storageCommand = new Command("storage");
        storageCommand.Subcommands.Add(vertexCommands.CreateCommand());
        storageCommand.Subcommands.Add(edgeCommands.CreateCommand());
        storageCommand.Subcommands.Add(edgeTypeCommands.CreateCommand());
        return storageCommand;
    }
}