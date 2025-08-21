namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.EdgeType;

using System.ComponentModel;
using Spectre.Console.Cli;

[Description("Listing edge types.")]
public sealed class ListEdgeTypeCommand : Command<ListEdgeTypeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("Listing edge type");
        return 0;
    }

    public sealed class Settings : EdgeTypeSettings
    {
    }
}