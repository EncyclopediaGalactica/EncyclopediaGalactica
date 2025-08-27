namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Vertex;

using System.ComponentModel;
using Spectre.Console.Cli;

public class DeleteVertexCommand : Command<DeleteVertexCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("update vertex");
        return 0;
    }

    public sealed class Settings : VertexSettings
    {
        [CommandOption("--vertex-type-id", true)]
        [Description("The ID of the vertex type.")]
        public long VertexTypeId { get; set; }
    }
}