namespace EncyclopediaGalactica.CommandLineInterface.Cli.Storage.VertexType;

using Spectre.Console.Cli;

public sealed class ListVertexTypeCommand : Command<ListVertexTypeCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        Console.WriteLine("add vertex type");
        return 0;
    }

    public sealed class Settings : VertexTypeSettings
    {
    }
}