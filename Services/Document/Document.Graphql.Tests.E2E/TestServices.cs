namespace Documents.Graphql.Tests.E2E;

public static class TestServices
{
    // static TestServices()
    // {
    //     SqliteConnection sqliteConnection = new("Filename=:memory:");
    //     sqliteConnection.Open();
    //     ServiceProvider = new ServiceCollection()
    //         .AddScoped<IDocumentService, DocumentService>()
    //         .AddScoped<IDocumentsRepository, DocumentRepository>()
    //         .AddScoped<IDocumentMappers, DocumentMappers>()
    //         .AddScoped<IGuardsService, GuardsService>()
    //         .AddScoped<ISourceFormatNodeService, SourceFormatNodeService>()
    //         .AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>()
    //         .AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>()
    //         .AddScoped<ISourceFormatNodeCacheService, SourceFormatNodeCacheService>()
    //         .AddScoped<ISourceFormatsService, SourceFormatsService>()
    //         .AddScoped<ISourceFormatsRepository, SourceFormatsRepository>()
    //         .AddScoped<ISourceFormatMappers, SourceFormatMappers>()
    //         .AddScoped<IValidator<SourceFormatNode>, SourceFormatNodeValidator>()
    //         .AddScoped<IValidator<SourceFormatNodeDto>, SourceFormatNodeDtoValidator>()
    //         .AddScoped<IValidator<EncyclopediaGalactica.Services.Document.Entities.Document>, DocumentValidator>()
    //         .AddLogging(log =>
    //         {
    //             log.ClearProviders();
    //             log.AddConsole();
    //             log.AddDebug();
    //         })
    //         .AddDbContext<DocumentDbContext>(options =>
    //         {
    //             options.UseSqlite(sqliteConnection);
    //             options.LogTo(m => Debug.WriteLine(m))
    //                 .EnableSensitiveDataLogging()
    //                 .EnableDetailedErrors();
    //         })
    //         .AddGraphQLServer()
    //         .AddQueryType<QueryType>()
    //         .AddMutationType<MutationType>()
    //         .RegisterService<IDocumentService>()
    //         .AddType<DocumentDtoType>()
    //         .Services
    //         .AddSingleton(sp => new RequestExecutorProxy(
    //             sp.GetRequiredService<IRequestExecutorResolver>(),
    //             Schema.DefaultName))
    //         .BuildServiceProvider();
    //
    //     using (IServiceScope scope = ServiceProvider.CreateScope())
    //     {
    //         IServiceProvider scopedServices = scope.ServiceProvider;
    //         DocumentDbContext dbContext = scopedServices.GetRequiredService<DocumentDbContext>();
    //         dbContext.Database.EnsureDeleted();
    //         dbContext.Database.EnsureCreated();
    //     }
    //
    //     RequestExecutorProxy = ServiceProvider.GetRequiredService<RequestExecutorProxy>();
    // }
    //
    // public static IServiceProvider ServiceProvider { get; set; }
    // public static RequestExecutorProxy RequestExecutorProxy { get; set; }
    //
    // public static async Task<IQueryResult> ExecuteRequestAsync(
    //     Action<IQueryRequestBuilder> configureRequest,
    //     CancellationToken cancellationToken = default)
    // {
    //     await using var scope = ServiceProvider.CreateAsyncScope();
    //
    //     var requestBuilder = new QueryRequestBuilder();
    //     requestBuilder.SetServices(scope.ServiceProvider);
    //     configureRequest(requestBuilder);
    //     var request = requestBuilder.Create();
    //
    //     await using IExecutionResult result = await RequestExecutorProxy.ExecuteAsync(request, cancellationToken);
    //
    //     IQueryResult res = result.ExpectQueryResult();
    //     return res;
    // }
}