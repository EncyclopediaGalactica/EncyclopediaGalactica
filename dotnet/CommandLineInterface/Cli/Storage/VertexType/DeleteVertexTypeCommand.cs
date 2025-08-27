namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.VertexType;

using System.ComponentModel;
using Spectre.Console.Cli;

public sealed class DeleteVertexTypeCommand : Command<DeleteVertexTypeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("add vertex type");
        return 0;
    }

    public sealed class Settings : VertexTypeSettings
    {
        [CommandOption("--id ", true)]
        [Description("The id of the vertex type")]
        public long VertexTypeId { get; set; }
    }
}