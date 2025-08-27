namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.EdgeType;

using System.ComponentModel;
using Spectre.Console.Cli;

[Description("Updating edge type.")]
public sealed class UpdateEdgeTypeCommand : Command<UpdateEdgeTypeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("Updating edge type.");
        return 0;
    }

    public sealed class Settings : EdgeTypeSettings
    {
        [CommandOption("--id", true)]
        [Description("The id of the edge type.")]
        public long EdgeTypeId { get; set; }
    }
}