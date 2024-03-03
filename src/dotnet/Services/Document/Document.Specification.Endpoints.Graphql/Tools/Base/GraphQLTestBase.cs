namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tools.Base;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Arguments.ErrorFilters;
using Arguments.Types.Input;
using Arguments.Types.Mutations;
using Arguments.Types.Queries;
using Arguments.Types.Result;
using Contracts.Input;
using Ctx;
using Entities;
using FluentValidation;
using HotChocolate.Execution;
using Mappers;
using Mappers.Document;
using Mappers.Interfaces;
using Mappers.SourceFormatNode;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Document;
using Repository.Interfaces;
using Repository.SourceFormatNode;
using Service.Document;
using Service.Interfaces.Document;
using Utils.GuardsService;
using Utils.GuardsService.Interfaces;
using ValidatorService;
using Xunit.Abstractions;

public partial class GraphQLTestBase
{
    protected readonly ITestOutputHelper _testOutputHelper;

    public GraphQLTestBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        SqliteConnection sqliteConnection = new("Filename=:memory:");
        sqliteConnection.Open();
        ServiceProvider = new ServiceCollection()
            .AddScoped<IDeleteDocumentScenario, DeleteDocumentScenario>()
            .AddScoped<IAddDocumentScenario, AddDocumentScenario>()
            .AddScoped<IGetAllDocumentsScenario, GetAllDocumentsScenario>()
            .AddScoped<IGetDocumentByIdScenario, GetDocumentByIdScenario>()
            .AddScoped<IUpdateDocumentScenario, UpdateDocumentScenario>()
            .AddScoped<IDocumentsRepository, DocumentRepository>()
            .AddScoped<IDocumentMappers, DocumentMappers>()
            .AddScoped<IGuardsService, GuardsService>()
            .AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>()
            .AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>()
            .AddScoped<ISourceFormatsRepository, SourceFormatsRepository>()
            .AddScoped<ISourceFormatMappers, SourceFormatMappers>()
            .AddScoped<IValidator<SourceFormatNode>, SourceFormatNodeValidator>()
            .AddScoped<IValidator<SourceFormatNodeInput>, SourceFormatNodeDtoValidator>()
            .AddScoped<IValidator<Contracts.Input.DocumentInput>, DocumentInputValidator>()
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
            .AddQueryType<Query>()
            .AddTypeExtension<GetDocumentsQuery>()
            .AddMutationType<Mutation>()
            .AddTypeExtension<AddDocumentMutation>()
            .AddTypeExtension<DeleteDocumentMutation>()
            .AddTypeExtension<UpdateDocumentMutation>()
            .RegisterService<IDeleteDocumentScenario>()
            .RegisterService<IAddDocumentScenario>()
            .RegisterService<IGetAllDocumentsScenario>()
            .RegisterService<IGetDocumentByIdScenario>()
            .RegisterService<IUpdateDocumentScenario>()
            .AddType<DocumentOutput>()
            .AddType<DocumentInputType>()
            .AddErrorFilter<GraphQlSchemaValidationErrorFilter>()
            .AddErrorFilter<InputValidationErrorFilter>()
            .AddErrorFilter<NoSuchItemErrorFilter>()
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

    public IServiceProvider ServiceProvider { get; set; }
    public RequestExecutorProxy RequestExecutorProxy { get; set; }

    protected async Task<string> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        ITestOutputHelper? testOutputHelper = default,
        CancellationToken cancellationToken = default)
    {
        await using var scope = ServiceProvider.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        IQueryRequest request = requestBuilder.Create();

        RequestDetails(testOutputHelper, request);

        await using IExecutionResult result = await RequestExecutorProxy.ExecuteAsync(request, cancellationToken);
        {
            QueryResult queryResult = (QueryResult)result;
            if (testOutputHelper is not null
                && queryResult.Errors is not null
                && queryResult.Errors.Any())
            {
                DumpErrorInformation(queryResult.Errors, testOutputHelper);
            }

            return result.ToJson();
        }
    }

    private void DumpErrorInformation(
        IReadOnlyList<IError> queryResultErrors,
        ITestOutputHelper testOutputHelper)
    {
        StringBuilder builder = new StringBuilder();
        string header = "==============\n" +
                        "=== Errors ===\n" +
                        "==============\n";
        builder.Append(header);
        foreach (IError error in queryResultErrors)
        {
            string msg = $"=== Error ===\n" +
                         $"== Error name: {error.Message} \n" +
                         $"== Inner Exception: {error.Exception?.GetType()} \n" +
                         $"== Inner Exception Message: {error.Exception?.Message} \n";
            builder.Append(msg);
        }

        testOutputHelper.WriteLine(builder.ToString());
    }


    private static void RequestDetails(ITestOutputHelper? testOutputHelper, IQueryRequest request)
    {
        if (testOutputHelper != null)
        {
            testOutputHelper.WriteLine("=== Request Info ===");

            foreach (PropertyInfo propertyInfo in request.GetType().GetProperties())
            {
                if (propertyInfo.Name == "VariableValues")
                {
                    if (request.VariableValues != null && request.VariableValues.Any())
                    {
                        testOutputHelper.WriteLine("== Variable values");
                        foreach (KeyValuePair<string, object?> pair in request.VariableValues)
                        {
                            if (pair.Value?.GetType() == typeof(ReadOnlyDictionary<string, object>))
                            {
                                IReadOnlyDictionary<string, object> valDict =
                                    pair.Value as IReadOnlyDictionary<string, object>;
                                foreach (KeyValuePair<string, object?> kv in valDict)
                                {
                                    string kvm = $"== {pair.Key} -  {kv.Key}: {kv.Value}";
                                    testOutputHelper.WriteLine(kvm);
                                }

                                continue;
                            }

                            string varVal = $"== {pair.Key}: {(pair.Value != null ? pair.Value.ToString() : "Null")}";
                            testOutputHelper.WriteLine(varVal);
                        }

                        continue;
                    }

                    testOutputHelper.WriteLine($"Not provided");
                    continue;
                }

                string msg = $"= {propertyInfo.Name}: {propertyInfo.GetValue(request)}";
                testOutputHelper.WriteLine(msg);
            }
        }
    }
}