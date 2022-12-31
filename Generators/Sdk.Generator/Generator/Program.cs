namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator;

using System.CommandLine;
using Generator;

class Program
{
    public static async Task<int> Main(string[] args)
    {
        Option<string?> configuration = new Option<string?>("--configuration")
        {
            Description = "The path to the OpenApi specification file.",
            // IsRequired = true
        };
        configuration.AddAlias("-c");


        RootCommand rootCommand = new RootCommand(
            $"=== Encyclopedia Galactica SDK Generator === \n" +
            $"This tool generates all the necessary code needed to be able to\n" +
            $"connect to the Encyclopedia Galactica Endpoints."
        );
        rootCommand.AddOption(configuration);

        rootCommand.SetHandler((configurationFileNameAndPath) =>
            {
                if (string.IsNullOrEmpty(configurationFileNameAndPath))
                {
                    Console.WriteLine("ERROR === Path to configuration file is not set up.");
                    return;
                }

                new CodeGenerator.Builder()
                    .SetPath(configurationFileNameAndPath)
                    .Generate();
            },
            configuration);

        return await rootCommand.InvokeAsync(args);
    }
}