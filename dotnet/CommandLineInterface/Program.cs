namespace EncyclopediaGalactica.CommandLineInterface;

using Cli;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scenarios.Exercises.Book.AddNew;
using Scenarios.Exercises.Book.Find;
using Scenarios.Exercises.Book.UpdateBook;
using Scenarios.Exercises.Chapter.Add;
using Scenarios.Exercises.Chapter.Find;
using Scenarios.Exercises.Chapter.Update;
using Scenarios.Exercises.Exercise.UpdateOrInsert;
using Scenarios.Exercises.Generate;
using Scenarios.Exercises.Logic.Repository;
using Scenarios.Exercises.Logic.Repository.Book;
using Scenarios.Exercises.Logic.Repository.Chapter;
using Scenarios.Exercises.Logic.Repository.Exercise;
using Scenarios.Exercises.Logic.Repository.Section;
using Scenarios.Exercises.Logic.Repository.Topic;
using Scenarios.Exercises.Logic.Sync;
using Scenarios.Exercises.Section.Add;
using Scenarios.Exercises.Section.Find;
using Scenarios.Exercises.Topic.Add;
using Scenarios.Exercises.Topic.Find;
using Scenarios.Storage.Edge;
using Scenarios.Storage.EdgeType;
using Spectre.Console.Cli;
using Storage;
using Storage.Repository.Edge;
using Storage.Repository.EdgeType;

internal class Program
{
    private static int Main(string[] args)
    {
        ServiceCollection services = new();
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();
        services.AddSingleton(configuration);
        AppSettings appSettings = configuration.GetSection("AppSettings").Get<AppSettings>()!;
        services.AddSingleton(appSettings);

        // scenarios
        services.AddTransient<AddEdgeTypeScenario>();
        services.AddTransient<GetAllEdgeTypesScenario>();
        services.AddTransient<AddEdgeScenario>();
        services.AddTransient<GetAllEdgesScenario>();

        // repositories
        services.AddTransient<EdgeTypeRepository>();
        services.AddTransient<EdgeRepository>();

        // Exercises
        services.AddDbContext<ExercisesContext>(options =>
            {
                string? connectionString = services.BuildServiceProvider()
                    .GetService<AppSettings>()!
                    .Exercises!.ConnectionStrings!.DefaultConnection;
                options.UseNpgsql(connectionString);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            }
        );
        using ExercisesContext? exerciseContext = services.BuildServiceProvider().GetService<ExercisesContext>();
        exerciseContext!.Database.EnsureDeleted();
        exerciseContext!.Database.EnsureCreated();

        services.AddTransient<FindTopicByNameAndReferenceScenario>();
        services.AddTransient<FindBookByTopicIdAndReferenceScenario>();
        services.AddTransient<AddNewTopicScenario>();
        services.AddTransient<AddNewTopicScenarioInputValidator>();
        services.AddTransient<UpdateBookScenario>();
        services.AddTransient<UpdateBookScenarioInputValidator>();
        services.AddTransient<FindChapterByBookIdAndReferenceScenario>();
        services.AddTransient<AddNewChapterScenario>();
        services.AddTransient<AddNewChapterScenarioInputValidator>();
        services.AddTransient<UpdateChapterScenario>();
        services.AddTransient<UpdateChapterScenarioInputValidator>();
        services.AddTransient<FindSectionByChapterIdAndSectionNumberScenario>();
        services.AddTransient<AddNewSectionScenario>();
        services.AddTransient<AddNewSectionScenarioInputValidation>();
        services.AddTransient<GetAllTopicsScenario>();
        services.AddTransient<UpdateOrInsertExerciseScenario>();
        services.AddTransient<UpdateExerciseScenarioInputValidator>();
        services.AddTransient<AddExerciseScenarioInputValidator>();
        services.AddTransient<FindTopicByReferenceScenario>();
        services.AddTransient<FindBookByReferenceScenario>();
        services.AddTransient<GenerateFromBooksScenario>();
        services.AddTransient<FindChapterByReferenceScenario>();
        services.AddTransient<GenerateFromBooksScenario>();

        // Add new book scenario
        services.AddTransient<AddNewBookByTopicIdAndParsedBook>();
        services.AddTransient<AddNewBookScenarioInputValidator>();

        // repositories
        services.AddTransient<BookRepository>();
        services.AddTransient<SyncFsWithDb>();
        services.AddTransient<TopicRepository>();
        services.AddTransient<ChapterRepository>();
        services.AddTransient<SectionRepository>();
        services.AddTransient<ExerciseRepository>();

        // validators
        services.AddTransient<AddEdgeTypeScenarioInputValidator>();
        services.AddTransient<AddEdgeScenarioInputValidator>();

        // EG db context
        services.AddDbContext<StorageContext>(options =>
            {
                string? connectionString = services.BuildServiceProvider().GetService<AppSettings>()!
                    .ConnectionStrings!.DefaultConnection;
                options.UseNpgsql(connectionString);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        );
        // reset the database before the app starts
        // using StorageContext? context = services.BuildServiceProvider().GetService<StorageContext>();
        // context!.Database.EnsureDeleted();
        // context!.Database.EnsureCreated();
        TypeRegistrar registrar = new(services);
        CommandApp app = EgCli.CreateCommandApp(registrar);
        return app.Run(args);
    }
}