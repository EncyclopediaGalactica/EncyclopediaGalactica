using System.Diagnostics;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.Graphql.Types.Document;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Document;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.SourceFormatNodeService;
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
    .AddScoped<IDocumentsRepository, DocumentRepository>()
    .AddScoped<IDocumentMappers, DocumentMappers>()
    .AddScoped<IGuardsService, GuardsService>();

// registered SourceFormatNode services
builder.Services
    .AddScoped<ISourceFormatNodeService, SourceFormatNodeService>()
    .AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>()
    .AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>()
    .AddScoped<ISourceFormatNodeCacheService, SourceFormatNodeCacheService>();

// registered SourceFormatServices
builder.Services
    .AddScoped<ISourceFormatsService, SourceFormatsService>()
    .AddScoped<ISourceFormatsRepository, SourceFormatsRepository>()
    .AddScoped<ISourceFormatMappers, SourceFormatMappers>();

// registered validators
builder.Services
    .AddScoped<IValidator<SourceFormatNode>, SourceFormatNodeValidator>()
    .AddScoped<IValidator<SourceFormatNodeDto>, SourceFormatNodeDtoValidator>()
    .AddScoped<IValidator<EncyclopediaGalactica.Services.Document.Entities.Document>, DocumentValidator>()
    .AddScoped<IValidator<DocumentDto>, DocumentDtoValidator>();

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
    .AddQueryType<GetDocumentsQuery>()
    .AddMutationType<UpdateDocumentMutation>()
    .RegisterService<IDocumentService>()
    .AddType<DocumentDtoType>()
    .AddType<DocumentDtoInputType>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();