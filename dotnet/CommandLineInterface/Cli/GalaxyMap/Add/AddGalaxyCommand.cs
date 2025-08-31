namespace EncyclopediaGalactica.CommandLineInterface.Cli.GalaxyMap.Add;

using System.ComponentModel;
using Spectre.Console.Cli;

public sealed class AddGalaxyCommand : Command<AddGalaxyCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }

    public sealed class Settings : AddSettings
    {

        [CommandOption("-n|--name")]
        [Description("Name of the galaxy")]
        public string Name { get; set; } = string.Empty;

        [CommandOption("-d|--description")]
        [Description("Description of the galaxy")]
        public string Description { get; set; } = string.Empty;
    }
}