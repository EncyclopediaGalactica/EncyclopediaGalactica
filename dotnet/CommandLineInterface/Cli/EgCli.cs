namespace EncyclopediaGalactica.CommandLineInterface.Cli;

using System.CommandLine;
using Storage;

public class EgCli(
    StorageCommandOld storageCommandOld)
{
    public RootCommand Cli()
    {
        RootCommand egRootCommand = new("Encyclopedia Galactica CLI");
        egRootCommand.Subcommands.Add(storageCommandOld.CreateCommand());
        return egRootCommand;
    }
}