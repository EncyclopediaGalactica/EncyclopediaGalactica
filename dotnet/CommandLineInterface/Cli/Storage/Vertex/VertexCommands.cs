namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Vertex;

using System.CommandLine;

public class VertexCommands
{
    public Command CreateCommand()
    {
        Command vertexCommand = new Command("vertex");
        
        Command addCommand = new Command("add");
        vertexCommand.Subcommands.Add(addCommand);
        
        Command deleteCommand = new Command("delete");
        vertexCommand.Subcommands.Add(deleteCommand);
        
        Command listCommand = new Command("list");
        vertexCommand.Subcommands.Add(listCommand);
        
        Command updateCommand = new Command("update");
        vertexCommand.Subcommands.Add(updateCommand);
        
        return vertexCommand;
    }
}