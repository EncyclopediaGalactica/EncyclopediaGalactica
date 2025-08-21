namespace EncyclopediaGalactica.CommandLineInterface;

using Cli.Storage;
using Cli.Storage.Edge;
using Cli.Storage.EdgeType;
using Spectre.Console.Cli;

internal class Program
{
    private static int Main(string[] args)
    {
        CommandApp app = new();
        app.Configure(config =>
            {
                config.AddBranch<StorageSettings>(
                    "storage",
                    storage =>
                    {
                        storage.AddBranch<EdgeTypeSettings>(
                            "edge-type",
                            edgeType =>
                            {
                                edgeType.AddCommand<AddEdgeTypeCommand>("add");
                                edgeType.AddCommand<UpdateEdgeTypeCommand>("update");
                                edgeType.AddCommand<ListEdgeTypeCommand>("list");
                                edgeType.AddCommand<DeleteEdgeTypeCommand>("delete");
                            }
                        );
                        storage.AddBranch<EdgeSettings>(
                            "edge",
                            edge =>
                            {
                                edge.AddCommand<AddEdgeCommand>("add");
                            }
                        );
                    }
                );
            }
        );
        return app.Run(args);
        // HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        // builder.Configuration
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("appSettings.json", true)
        //     .AddEnvironmentVariables();
        //
        // // cli
        // builder.Services.AddTransient<EgCli>();
        // builder.Services.AddTransient<StorageCommandOld>();
        // builder.Services.AddTransient<VertexCommandsOld>();
        // builder.Services.AddTransient<EdgeCommands>();
        // builder.Services.AddTransient<EdgeTypeCommands>();
        //
        // // scenarios
        // builder.Services.AddTransient<AddEdgeTypeScenario>();
        // builder.Services.AddTransient<GetAllEdgeTypesScenario>();
        // builder.Services.AddTransient<AddEdgeScenario>();
        // builder.Services.AddTransient<GetAllEdgesScenario>();
        //
        // // repositories
        // builder.Services.AddTransient<EdgeTypeRepository>();
        // builder.Services.AddTransient<EdgeRepository>();
        //
        // // validators
        // builder.Services.AddTransient<AddEdgeTypeScenarioInputValidator>();
        // builder.Services.AddTransient<AddEdgeScenarioInputValidator>();
        //
        // // db context
        // builder.Services.AddDbContext<StorageContext>(options =>
        //     {
        //         options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        //         options.EnableDetailedErrors();
        //         options.EnableSensitiveDataLogging();
        //     }
        // );
        // using IHost host = builder.Build();
        // IServiceProvider serviceProvider = host.Services;
        //
        // // reset the database before the app starts
        // using StorageContext? context = serviceProvider.GetService<StorageContext>();
        // // context!.Database.EnsureDeleted();
        // // context!.Database.EnsureCreated();
        //
        // EgCli cli = serviceProvider.GetRequiredService<EgCli>();
        // RootCommand rootCommand = cli.Cli();
        //
        // return rootCommand.Parse(args).Invoke();
    }
}