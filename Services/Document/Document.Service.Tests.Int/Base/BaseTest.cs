namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Base;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using Ctx;
using Entities;
using FluentValidation;
using Interfaces;
using Interfaces.Document;
using Interfaces.SourceFormatNode;
using Mappers;
using Mappers.Document;
using Mappers.Interfaces;
using Mappers.SourceFormatNode;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Document;
using Repository.Interfaces;
using Repository.SourceFormatNode;
using Service.Document;
using Service.SourceFormatNodeService;
using Utils.GuardsService;
using ValidatorService;

[ExcludeFromCodeCoverage]
public partial class BaseTest
{
    protected readonly ISourceFormatsService Sut;

    public BaseTest()
    {
        SqliteConnection connection = new("Filename=:memory:");
        connection.Open();
        SourceFormatNodeDtoValidator validator = new();
        IValidator<SourceFormatNode> nodeValidator = new SourceFormatNodeValidator();
        ISourceFormatNodeMappers sourceFormatNodeMappers = new SourceFormatNodeMappers();
        IDocumentMappers documentMappers = new DocumentMappers();
        ISourceFormatMappers mappers = new SourceFormatMappers(
            sourceFormatNodeMappers,
            documentMappers);

        DbContextOptions<DocumentDbContext> dbContextOptions =
            new DbContextOptionsBuilder<DocumentDbContext>()
                .UseSqlite(connection).LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        DocumentDbContext ctx = new(dbContextOptions);
        ctx.Database.EnsureCreated();

        ISourceFormatNodeRepository sourceFormatNodeRepository = new SourceFormatNodeRepository(
            dbContextOptions, nodeValidator, new GuardsService());
        ILogger<SourceFormatNodeService> logger =
            new Logger<SourceFormatNodeService>(
                new LoggerFactory());
        ISourceFormatNodeService sourceFormatNodeService =
            new SourceFormatNodeService(
                validator,
                new GuardsService(),
                mappers,
                sourceFormatNodeRepository,
                logger);

        IValidator<Entities.Document> documentValidator = new DocumentValidator();
        IValidator<DocumentGraphqlInput> documentDtoValidator = new DocumentDtoValidator();
        IDocumentsRepository documentsRepository = new DocumentRepository(
            dbContextOptions, documentValidator);
        IDocumentService documentService = new DocumentService(
            new GuardsService(),
            mappers,
            documentsRepository,
            new DocumentDtoValidator());

        Sut = new SourceFormatsService(
            sourceFormatNodeService,
            documentService);
    }
}