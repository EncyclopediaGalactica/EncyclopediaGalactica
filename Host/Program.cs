using EncyclopediaGalactica.DocumentDomain.Infrastructure.Database;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Resolvers;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Schema;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Schema.DocumentType;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Types;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;
using EncyclopediaGalactica.DocumentDomain.Operations.Commands.DocumentType;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;
using Host.Seeders;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

SqliteConnection connection = new SqliteConnection("Filename=:memory:");
connection.Open();
DbContextOptions<DocumentDomainDbContext> dbContextOptions = new DbContextOptionsBuilder<DocumentDomainDbContext>()
    .UseSqlite(connection)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
    .Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services
    .AddDbContext<DocumentDomainDbContext>(o =>
    {
        o.UseSqlite(connection);
        using DocumentDomainDbContext ctx = new DocumentDomainDbContext(dbContextOptions);
        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
        DocumentTypeSeeder.SeedN(10, ctx);
    })
    .AddScoped<GetDocumentTypesScenario>()
    .AddScoped<GetDocumentTypesCommand>()
    .AddScoped<DocumentTypeMapper>();

// graphql
builder.Services
    .AddGraphQLServer()
    // Query types
    .AddQueryType<QueryType>()
    .AddTypeExtension<DocumentTypeQueries>()
    // mutation types
    .AddMutationType<Mutation>()
    .AddTypeExtension<DocumentTypeMutation>()
    // Types
    .AddType<DocumentTypeResultGqlType>()
    .AddType<DocumentTypeQueries>()
    // Resolvers
    .AddResolver<DocumentTypeResolver>()
    .RegisterService<GetDocumentTypesScenario>()
    .RegisterService<GetDocumentTypesCommand>()
    .RegisterService<DocumentTypeMapper>();

WebApplication app = builder.Build();
// register graphql things
app.MapGraphQL();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.Run();