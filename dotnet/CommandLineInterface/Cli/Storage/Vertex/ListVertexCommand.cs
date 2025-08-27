namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.Vertex;

using Spectre.Console.Cli;

public class ListVertexCommand : Command<ListVertexCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("update vertex");
        return 0;
    }

    public sealed class Settings : VertexSettings
    {
    }
}