namespace EncyclopediaGalactica.CommandLineInterface;

using System.CommandLine;
using Cli;
using Cli.Storage;
using Cli.Storage.Edge;
using Cli.Storage.EdgeType;
using Cli.Storage.Vertex;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage;

internal class Program
{
    static int Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true)
            .AddEnvironmentVariables();
        
        // cli
        builder.Services.AddTransient<EgCli>();
        builder.Services.AddTransient<StorageCommand>();
        builder.Services.AddTransient<VertexCommands>();
        builder.Services.AddTransient<EdgeCommands>();
        builder.Services.AddTransient<EdgeTypeCommands>();
        
        // db context
        builder.Services.AddDbContext<StorageContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
        using IHost host = builder.Build();
        IServiceProvider serviceProvider = host.Services;
        
        // reset the database before the app starts
        using StorageContext? context = serviceProvider.GetService<StorageContext>();
        context!.Database.EnsureDeleted();
        context!.Database.EnsureCreated();
        
        EgCli cli = serviceProvider.GetRequiredService<EgCli>();
        RootCommand rootCommand = cli.Cli();

        return rootCommand.Parse(args).Invoke();
    }
}