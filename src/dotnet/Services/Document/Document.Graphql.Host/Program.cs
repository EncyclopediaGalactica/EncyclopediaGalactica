using System.Diagnostics;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Input;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Mutations;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Queries;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Result;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Document;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Repository.Document;
using EncyclopediaGalactica.Services.Document.Repository.Interfaces;
using EncyclopediaGalactica.Services.Document.Service.Document;
using EncyclopediaGalactica.Services.Document.Service.Interfaces.Document;
using EncyclopediaGalactica.Services.Document.Service.Interfaces.Structure;
using EncyclopediaGalactica.Services.Document.Service.Structure;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using EncyclopediaGalactica.Utils.GuardsService.Interfaces;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// registered Document services
builder.Services
    .AddScoped<IDeleteDocumentScenario, DeleteDocumentScenario>()
    .AddScoped<IAddDocumentScenario, AddDocumentScenario>()
    .AddScoped<IGetAllDocumentsScenario, GetAllDocumentsScenario>()
    .AddScoped<IGetDocumentByIdScenario, GetDocumentByIdScenario>()
    .AddScoped<IUpdateDocumentScenario, UpdateDocumentScenario>()
    .AddScoped<IGetStructureNodeScenario, GetStructureNodeScenario>()
    .AddScoped<IDocumentsRepository, DocumentRepository>()
    .AddScoped<ISourceFormatMappers, SourceFormatMappers>()
    .AddScoped<IDocumentMappers, DocumentMappers>()
    .AddScoped<IGuardsService, GuardsService>();

// registered validators
builder.Services
    .AddScoped<IValidator<EncyclopediaGalactica.Services.Document.Entities.Document>, DocumentValidator>()
    .AddScoped<IValidator<EncyclopediaGalactica.Services.Document.Contracts.Input.DocumentInput>,
        DocumentInputValidator>();

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
    db.Database.EnsureCreated();
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
    .RegisterService<IGetStructureNodeScenario>()
    .AddType<DocumentOutput>()
    .AddType<DocumentInputType>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();