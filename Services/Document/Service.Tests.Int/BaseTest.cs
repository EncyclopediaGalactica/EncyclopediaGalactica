namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Document;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ExcludeFromCodeCoverage]
public class BaseTest
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

        DbContextOptions<SourceFormatsDbContext> dbContextOptions =
            new DbContextOptionsBuilder<SourceFormatsDbContext>()
                .UseSqlite(connection).LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        SourceFormatsDbContext ctx = new(dbContextOptions);
        ctx.Database.EnsureCreated();

        ISourceFormatNodeRepository sourceFormatNodeRepository = new SourceFormatNodeRepository(
            dbContextOptions, nodeValidator, new GuardsService());
        ISourceFormatNodeCacheService sourceFormatNodeCacheService = new SourceFormatNodeCacheService();
        ILogger<Services.Document.SourceFormatsService.SourceFormatNodeService.SourceFormatNodeService> logger =
            new Logger<Services.Document.SourceFormatsService.SourceFormatNodeService.SourceFormatNodeService>(
                new LoggerFactory());
        ISourceFormatNodeService sourceFormatNodeService =
            new Services.Document.SourceFormatsService.SourceFormatNodeService.SourceFormatNodeService(
                validator,
                new GuardsService(),
                mappers,
                sourceFormatNodeRepository,
                sourceFormatNodeCacheService,
                logger);

        IValidator<Entities.Document> documentValidator = new DocumentValidator();
        IDocumentsRepository documentsRepository = new DocumentRepository(
            dbContextOptions, documentValidator);
        IDocumentService documentService = new DocumentService(
            new GuardsService(),
            mappers,
            documentsRepository);

        Sut = new SourceFormatsService(
            sourceFormatNodeService,
            documentService);
    }
}