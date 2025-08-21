namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.EdgeType;

using System.ComponentModel;
using Spectre.Console.Cli;

[Description("Adding new edge type.")]
public sealed class AddEdgeTypeCommand : Command<AddEdgeTypeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("Adding edge type");
        return 0;
    }

    public sealed class Settings : EdgeTypeSettings
    {
        [CommandOption("-n|--name")]
        [Description("The name of the edge type")]
        public string Name { get; set; }
    }
}