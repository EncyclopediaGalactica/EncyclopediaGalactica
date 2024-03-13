using System.Diagnostics;
using EncyclopediaGalactica.BusinessLogic.Commands.Document;
using EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;
using EncyclopediaGalactica.BusinessLogic.Contracts;
using EncyclopediaGalactica.BusinessLogic.Database;
using EncyclopediaGalactica.BusinessLogic.Mappers;
using EncyclopediaGalactica.BusinessLogic.Sagas.Document;
using EncyclopediaGalactica.BusinessLogic.Sagas.Interfaces;
using EncyclopediaGalactica.BusinessLogic.Validators;
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
    .AddScoped<IHaveInputAndResultSaga<DocumentResult, GetDocumentByIdContext>, GetDocumentByIdSaga>()
    // commands
    .AddScoped<IAddDocumentCommand, AddDocumentCommand>()
    .AddScoped<IAddStructureNodeTreeCommand, AddStructureNodeTreeCommand>()
    .AddScoped<IGetDocumentByIdCommand, GetDocumentByIdCommand>()
    .AddScoped<IGetAllDocumentsCommand, GetAllDocumentsCommand>()
    .AddScoped<IDeleteDocumentCommand, DeleteDocumentCommand>()
    .AddScoped<IUpdateDocumentCommand, UpdateDocumentCommand>()
    // validators
    .AddScoped<IValidator<DocumentInput>, DocumentInputValidator>()
    .AddScoped<IValidator<StructureNodeInput>, StructureNodeInputValidator>()
    // mappers
    .AddScoped<IDocumentMapper, DocumentMapper>()
    .AddScoped<IStructureNodeMapper, StructureNodeMapper>()
    // seeder
    .AddScoped<IDocumentDataSeeder, DocumentDataSeeder>()
    ;

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
    .AddTypeExtension<GetDocuments>()
    .AddTypeExtension<GetDocumentById>()
    // mutations
    .AddMutationType<Mutation>()
    .AddTypeExtension<AddDocument>()
    .AddTypeExtension<DeleteDocument>()
    .AddTypeExtension<UpdateDocument>()
    // types
    .AddType<DocumentOutput>()
    .AddType<DocumentInputType>()
    // sagas
    .RegisterService<IHaveInputAndResultSaga<DocumentResult, AddDocumentSagaContext>>()
    .RegisterService<IHaveInputSaga<DeleteDocumentSagaContext>>()
    .RegisterService<IHaveInputAndResultSaga<DocumentResult, UpdateDocumentSagaContext>>()
    .RegisterService<IHaveResultSaga<List<DocumentResult>>>()
    .RegisterService<IHaveInputAndResultSaga<DocumentResult, GetDocumentByIdContext>>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    try
    {
        IServiceProvider scopedServices = scope.ServiceProvider;
        IDocumentDataSeeder documentDataSeeder = scopedServices.GetRequiredService<IDocumentDataSeeder>();
        await documentDataSeeder.SeedDocuments(1).ConfigureAwait(false);
    }
    catch (Exception e)
    {
        Console.WriteLine("=== Failed data seeding ===");
        Console.WriteLine(e.Message);
        Console.WriteLine(e.StackTrace);
        Console.WriteLine("===========================");
    }
}

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();