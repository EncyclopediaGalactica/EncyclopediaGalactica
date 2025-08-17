namespace EncyclopediaGalactica.CommandLineInterface.Cli;

using System.CommandLine;
using Storage;

public class EgCli(StorageCommand storageCommand)
{
   public RootCommand Cli()
   {
      RootCommand egRootCommand = new RootCommand("Encyclopedia Galactica CLI");
      egRootCommand.Subcommands.Add(storageCommand.CreateCommand());
      return egRootCommand;
   } 
    
}