using System.Diagnostics;
using EncyclopediaGalactica.Infrastructure.Graphql.RootTypes;
using EncyclopediaGalactica.Services.Document.Contracts.Input;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Input;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Mutations;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Queries;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Result;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Document;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Mappers.Structure;
using EncyclopediaGalactica.Services.Document.Repository.Document;
using EncyclopediaGalactica.Services.Document.Repository.Interfaces;
using EncyclopediaGalactica.Services.Document.Repository.Structure;
using EncyclopediaGalactica.Services.Document.Scenario.Document;
using EncyclopediaGalactica.Services.Document.Scenario.Interfaces.Document;
using EncyclopediaGalactica.Services.Document.Scenario.Interfaces.StructureNode;
using EncyclopediaGalactica.Services.Document.Scenario.StructureNode;
using EncyclopediaGalactica.Services.Document.Tests.Tools;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using EncyclopediaGalactica.Utils.GuardsService.Interfaces;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// registered Document services
builder.Services
    // document scenarios
    .AddScoped<IDeleteDocumentScenario, DeleteDocumentScenario>()
    .AddScoped<IAddDocumentScenario, AddDocumentScenario>()
    .AddScoped<IGetAllDocumentsScenario, GetAllDocumentsScenario>()
    .AddScoped<IGetDocumentByIdScenario, GetDocumentByIdScenario>()
    .AddScoped<IUpdateDocumentScenario, UpdateDocumentScenario>()
    // structure node scenarios
    .AddScoped<IGetRootStructureNodeByDocumentIdScenario, GetRootStructureNodeByDocumentIdByDocumentIdScenario>()
    .AddScoped<IAddNewRootStructureNodeScenario, AddNewRootStructureNodeScenario>()
    // repositories
    .AddScoped<IDocumentsRepository, DocumentRepository>()
    .AddScoped<IStructureNodeRepository, StructureNodeRepository>()
    // mappers
    .AddScoped<ISourceFormatMappers, SourceFormatMappers>()
    .AddScoped<IStructureNodeMappers, StructureNodeMappers>()
    .AddScoped<IDocumentMappers, DocumentMappers>()
    .AddScoped<IGuardsService, GuardsService>()
    .AddScoped<IDocumentDataSeeder, DocumentDataSeeder>();

// registered validators
builder.Services
    .AddScoped<IValidator<Document>, DocumentValidator>()
    .AddScoped<IValidator<DocumentInput>, DocumentInputValidator>()
    .AddScoped<IValidator<StructureNode>, StructureNodeValidator>();

// database
SqliteConnection connection = new("Filename=:memory:");
connection.Open();
builder.Services.AddDbContext<DocumentDbContext>(options =>
{
    options.UseSqlite(connection);
    options.LogTo(m => Debug.WriteLine(m))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});
ServiceProvider? sp = builder.Services.BuildServiceProvider();
if (sp is null)
    throw new ArgumentNullException(nameof(sp));

using (IServiceScope scope = sp.CreateScope())
{
    IServiceProvider scopedServices = scope.ServiceProvider;
    DocumentDbContext db = scopedServices.GetRequiredService<DocumentDbContext>();
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();
}

// logging
builder.Services.AddLogging(log =>
{
    log.ClearProviders();
    log.AddConsole();
    log.AddDebug();
});

// graphql related settings
builder.Services
    .AddGraphQLServer()
    // queries
    .AddQueryType<Query>()
    .AddTypeExtension<GetDocumentsQuery>()
    // mutations
    .AddMutationType<Mutation>()
    .AddTypeExtension<AddDocumentMutation>()
    .AddTypeExtension<DeleteDocumentMutation>()
    .AddTypeExtension<UpdateDocumentMutation>()
    .RegisterService<IDeleteDocumentScenario>()
    .RegisterService<IAddDocumentScenario>()
    .RegisterService<IGetAllDocumentsScenario>()
    .RegisterService<IGetDocumentByIdScenario>()
    .RegisterService<IUpdateDocumentScenario>()
    .RegisterService<IGetRootStructureNodeByDocumentIdScenario>()
    .RegisterService<IAddNewRootStructureNodeScenario>()
    .RegisterService<IDocumentDataSeeder>()
    .AddType<DocumentOutput>()
    .AddType<DocumentInputType>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    try
    {
        IServiceProvider scopedServices = scope.ServiceProvider;
        IDocumentDataSeeder documentDataSeeder = scopedServices.GetRequiredService<IDocumentDataSeeder>();
        await documentDataSeeder.SeedDocumentsWithRootStructureNode(5, 1);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine(e.StackTrace);
    }
}

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();