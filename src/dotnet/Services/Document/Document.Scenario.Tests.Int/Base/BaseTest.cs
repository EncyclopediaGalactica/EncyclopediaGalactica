namespace EncyclopediaGalactica.Services.Document.Scenario.Tests.Int.Base;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using Ctx;
using FluentValidation;
using Interfaces.Document;
using Mappers;
using Mappers.Document;
using Mappers.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repository.Document;
using Repository.Interfaces;
using Scenario.Document;
using Utils.GuardsService;
using ValidatorService;

[ExcludeFromCodeCoverage]
public partial class BaseTest
{
    protected readonly IAddDocumentScenario AddDocumentScenario;
    protected readonly IDeleteDocumentScenario DeleteDocumentScenario;
    protected readonly IGetAllDocumentsScenario GetAllDocumentsScenario;
    protected readonly IGetDocumentByIdScenario GetDocumentByIdScenario;
    protected readonly IUpdateDocumentScenario UpdateDocumentScenario;

    public BaseTest()
    {
        SqliteConnection connection = new("Filename=:memory:");
        connection.Open();
        SourceFormatNodeDtoValidator validator = new();
        IDocumentMappers documentMappers = new DocumentMappers();
        ISourceFormatMappers mappers = new SourceFormatMappers(documentMappers);

        DbContextOptions<DocumentDbContext> dbContextOptions =
            new DbContextOptionsBuilder<DocumentDbContext>()
                .UseSqlite(connection).LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        DocumentDbContext ctx = new(dbContextOptions);
        ctx.Database.EnsureCreated();

        IValidator<Entities.Document> documentValidator = new DocumentValidator();
        IValidator<DocumentInput> documentDtoValidator = new DocumentInputValidator();
        IDocumentsRepository documentsRepository = new DocumentRepository(
            dbContextOptions, documentValidator);
        DeleteDocumentScenario = new DeleteDocumentScenario(
            new GuardsService(),
            mappers,
            documentsRepository,
            new DocumentInputValidator());

        AddDocumentScenario = new AddDocumentScenario(
            new GuardsService(),
            mappers,
            documentsRepository,
            new DocumentInputValidator());

        GetAllDocumentsScenario = new GetAllDocumentsScenario(mappers, documentsRepository);
        GetDocumentByIdScenario = new GetDocumentByIdScenario(mappers, documentsRepository, new GuardsService());
        UpdateDocumentScenario = new UpdateDocumentScenario(new GuardsService(), mappers, documentsRepository,
            new DocumentInputValidator());
    }
}