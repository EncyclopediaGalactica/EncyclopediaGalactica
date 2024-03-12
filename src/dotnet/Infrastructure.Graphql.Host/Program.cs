using System.Diagnostics;
using EncyclopediaGalactica.BusinessLogic.Commands.Document;
using EncyclopediaGalactica.BusinessLogic.Contracts;
using EncyclopediaGalactica.BusinessLogic.Database;
using EncyclopediaGalactica.BusinessLogic.Mappers;
using EncyclopediaGalactica.BusinessLogic.Sagas.Document;
using EncyclopediaGalactica.BusinessLogic.Sagas.Interfaces;
using EncyclopediaGalactica.Infrastructure.Graphql.Types.Input;
using EncyclopediaGalactica.Infrastructure.Graphql.Types.Mutations;
using EncyclopediaGalactica.Infrastructure.Graphql.Types.Queries;
using EncyclopediaGalactica.Infrastructure.Graphql.Types.Result;
using EncyclopediaGalactica.Tools;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// registered Document services
builder.Services
    // sagas
    .AddScoped<IHaveInputAndResultSaga<DocumentResult, AddDocumentSagaContext>, AddDocumentSaga>()
    .AddScoped<IHaveInputSaga<DeleteDocumentSagaContext>, DeleteDocumentSaga>()
    .AddScoped<IHaveInputAndResultSaga<DocumentResult, UpdateDocumentSagaContext>, UpdateDocumentSaga>()
    .AddScoped<IHaveResultSaga<List<DocumentResult>>, GetDocumentsSaga>()
    // commands
    .AddScoped<IAddDocumentCommand, AddDocumentCommand>()
    .AddScoped<IGetDocumentByIdCommand, GetDocumentByIdCommand>()
    .AddScoped<IGetAllDocumentsCommand, GetAllDocumentsCommand>()
    .AddScoped<IDeleteDocumentCommand, DeleteDocumentCommand>()
    .AddScoped<IUpdateDocumentCommand, UpdateDocumentCommand>()
    // validators
    .AddScoped<IValidator<DocumentInput>>()
    // mappers
    .AddScoped<IDocumentMapper, DocumentMapper>();

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
    // types
    .AddType<DocumentOutput>()
    .AddType<DocumentInputType>()
    // sagas
    .RegisterService<IHaveInputAndResultSaga<DocumentResult, AddDocumentSagaContext>>()
    .RegisterService<IHaveInputSaga<DeleteDocumentSagaContext>>()
    .RegisterService<IHaveInputAndResultSaga<DocumentResult, UpdateDocumentSagaContext>>()
    .RegisterService<IHaveResultSaga<List<DocumentResult>>>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    try
    {
        IServiceProvider scopedServices = scope.ServiceProvider;
        IDocumentDataSeeder documentDataSeeder = scopedServices.GetRequiredService<IDocumentDataSeeder>();
        await documentDataSeeder.SeedDocuments(2);
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