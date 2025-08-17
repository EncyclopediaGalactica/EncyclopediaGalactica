namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Edge;

using System.CommandLine;

public class EdgeCommands
{
    public Command CreateCommand()
    {
        Command edgeCommand = new Command("edge");
        
        Command addCommand= new Command("add");
        edgeCommand.Subcommands.Add(addCommand);
        
        Command deleteCommand = new Command("delete");
        edgeCommand.Subcommands.Add(deleteCommand);
        
        Command listCommand = new Command("list");
        edgeCommand.Subcommands.Add(listCommand);
        
        Command updateCommand = new Command("update");
        edgeCommand.Subcommands.Add(updateCommand);
        return edgeCommand;
    }
}