namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using Entities;
using FluentValidation;
using Interfaces;
using Interfaces.SourceFormatNode;
using Mappers;
using Mappers.Interfaces;
using Mappers.SourceFormatNode;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using Repository.SourceFormatNode;
using SourceFormatsCacheService.Interfaces;
using SourceFormatsCacheService.SourceFormatNode;
using Utils.GuardsService;
using ValidatorService;

[ExcludeFromCodeCoverage]
public class BaseTest
{
    protected readonly ISourceFormatsService _sourceFormatsService;

    public BaseTest()
    {
        SqliteConnection connection = new("Filename=:memory:");
        connection.Open();
        SourceFormatNodeDtoValidator validator = new();
        IValidator<SourceFormatNode> nodeValidator = new SourceFormatNodeValidator();
        ISourceFormatNodeMappers sourceFormatNodeMappers = new SourceFormatNodeMappers();
        ISourceFormatMappers mappers = new SourceFormatMappers(sourceFormatNodeMappers);
        DbContextOptions<SourceFormatsDbContext> dbContextOptions =
            new DbContextOptionsBuilder<SourceFormatsDbContext>()
                .UseSqlite(connection).LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        SourceFormatsDbContext ctx = new(dbContextOptions);
        ctx.Database.EnsureCreated();
        ISourceFormatNodeRepository sourceFormatNodeRepository = new SourceFormatNodeRepository(
            ctx, nodeValidator, new GuardsService());
        ISourceFormatNodeCacheService sourceFormatNodeCacheService = new SourceFormatNodeCacheService();
        ILogger<SourceFormats.SourceFormatsService.SourceFormatNodeService.SourceFormatNodeService> logger =
            new Logger<SourceFormats.SourceFormatsService.SourceFormatNodeService.SourceFormatNodeService>(
                new LoggerFactory());
        ISourceFormatNodeService sourceFormatNodeService =
            new SourceFormats.SourceFormatsService.SourceFormatNodeService.SourceFormatNodeService(
                validator,
                new GuardsService(),
                mappers,
                sourceFormatNodeRepository,
                sourceFormatNodeCacheService,
                logger);
        _sourceFormatsService = new SourceFormatsService(sourceFormatNodeService);
    }
}