namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using Dtos;
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
using Services.Document.SourceFormatsService.Document;
using SourceFormatsCacheService.Interfaces;
using SourceFormatsCacheService.SourceFormatNode;
using SourceFormatsRepository.Document;
using SourceFormatsRepository.Interfaces;
using SourceFormatsRepository.SourceFormatNode;
using Utils.GuardsService;
using ValidatorService;

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

        DbContextOptions<DocumentDbContext> dbContextOptions =
            new DbContextOptionsBuilder<DocumentDbContext>()
                .UseSqlite(connection).LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        DocumentDbContext ctx = new(dbContextOptions);
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
        IValidator<DocumentDto> documentDtoValidator = new DocumentDtoValidator();
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