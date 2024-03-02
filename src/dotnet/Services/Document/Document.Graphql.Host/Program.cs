using System.Diagnostics;
using EncyclopediaGalactica.Services.Document.Contracts.Input;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.Graphql.Types.Mutations;
using EncyclopediaGalactica.Services.Document.Graphql.Types.Output;
using EncyclopediaGalactica.Services.Document.Graphql.Types.Queries;
using EncyclopediaGalactica.Services.Document.Graphql.Types.RootTypes;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Document;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Repository;
using EncyclopediaGalactica.Services.Document.Repository.Document;
using EncyclopediaGalactica.Services.Document.Repository.Interfaces;
using EncyclopediaGalactica.Services.Document.Repository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Service;
using EncyclopediaGalactica.Services.Document.Service.Document;
using EncyclopediaGalactica.Services.Document.Service.Interfaces;
using EncyclopediaGalactica.Services.Document.Service.Interfaces.Document;
using EncyclopediaGalactica.Services.Document.Service.Interfaces.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using EncyclopediaGalactica.Utils.GuardsService.Interfaces;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// registered Document services
builder.Services
    .AddScoped<IDocumentService, DocumentService>()
    .AddScoped<IAddDocumentScenario, AddDocumentScenario>()
    .AddScoped<IGetAllDocumentsScenario, GetAllDocumentsScenario>()
    .AddScoped<IDocumentsRepository, DocumentRepository>()
    .AddScoped<IDocumentMappers, DocumentMappers>()
    .AddScoped<IGuardsService, GuardsService>();

// registered SourceFormatNode services
builder.Services
    .AddScoped<ISourceFormatNodeService, SourceFormatNodeService>()
    .AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>()
    .AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>();

// registered SourceFormatServices
builder.Services
    .AddScoped<ISourceFormatsService, SourceFormatsService>()
    .AddScoped<ISourceFormatsRepository, SourceFormatsRepository>()
    .AddScoped<ISourceFormatMappers, SourceFormatMappers>();

// registered validators
builder.Services
    .AddScoped<IValidator<SourceFormatNode>, SourceFormatNodeValidator>()
    .AddScoped<IValidator<SourceFormatNodeInput>, SourceFormatNodeDtoValidator>()
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
    .RegisterService<IDocumentService>()
    .AddType<DocumentOutput>()
    .AddType<EncyclopediaGalactica.Services.Document.Graphql.Types.Input.DocumentInputType>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();