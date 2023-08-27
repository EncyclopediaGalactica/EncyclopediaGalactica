namespace Documents.Graphql.Tests.E2E;

using System.Diagnostics;
using Document.Graphql.Types;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Entities;
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
using HotChocolate.Execution;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class TestServices
{
    static TestServices()
    {
        SqliteConnection sqliteConnection = new("Filename=:memory:");
        sqliteConnection.Open();
        ServiceProvider = new ServiceCollection()
            .AddScoped<IDocumentService, DocumentService>()
            .AddScoped<IDocumentsRepository, DocumentRepository>()
            .AddScoped<IDocumentMappers, DocumentMappers>()
            .AddScoped<IGuardsService, GuardsService>()
            .AddScoped<ISourceFormatNodeService, SourceFormatNodeService>()
            .AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>()
            .AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>()
            .AddScoped<ISourceFormatNodeCacheService, SourceFormatNodeCacheService>()
            .AddScoped<ISourceFormatsService, SourceFormatsService>()
            .AddScoped<ISourceFormatsRepository, SourceFormatsRepository>()
            .AddScoped<ISourceFormatMappers, SourceFormatMappers>()
            .AddScoped<IValidator<SourceFormatNode>, SourceFormatNodeValidator>()
            .AddScoped<IValidator<SourceFormatNodeDto>, SourceFormatNodeDtoValidator>()
            .AddScoped<IValidator<EncyclopediaGalactica.Services.Document.Entities.Document>, DocumentValidator>()
            .AddLogging(log =>
            {
                log.ClearProviders();
                log.AddConsole();
                log.AddDebug();
            })
            .AddDbContext<DocumentDbContext>(options =>
            {
                options.UseSqlite(sqliteConnection);
                options.LogTo(m => Debug.WriteLine(m))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            })
            .AddGraphQLServer()
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .RegisterService<IDocumentService>()
            .AddType<DocumentDtoType>()
            .Services
            .AddSingleton(sp => new RequestExecutorProxy(
                sp.GetRequiredService<IRequestExecutorResolver>(),
                Schema.DefaultName))
            .BuildServiceProvider();

        using (IServiceScope scope = ServiceProvider.CreateScope())
        {
            IServiceProvider scopedServices = scope.ServiceProvider;
            DocumentDbContext dbContext = scopedServices.GetRequiredService<DocumentDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        RequestExecutorProxy = ServiceProvider.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider ServiceProvider { get; set; }
    public static RequestExecutorProxy RequestExecutorProxy { get; set; }

    public static async Task<IQueryResult> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        await using var scope = ServiceProvider.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        await using IExecutionResult result = await RequestExecutorProxy.ExecuteAsync(request, cancellationToken);

        IQueryResult res = result.ExpectQueryResult();
        return res;
    }
}