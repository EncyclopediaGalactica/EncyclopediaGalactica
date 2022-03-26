namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using Entities;
using FluentValidation;
using Guards;
using Interfaces;
using Mappers;
using Mappers.Interfaces;
using Mappers.SourceFormatNode;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.SourceFormatNode;
using SourceFormatsCacheService.Interfaces;
using SourceFormatsCacheService.SourceFormatNode;
using ValidatorService;

[ExcludeFromCodeCoverage]
public class BaseTest
{
    protected readonly ISourceFormatsService _sourceFormatsService;

    public BaseTest()
    {
        SqliteConnection connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        SourceFormatNodeDtoValidator validator = new SourceFormatNodeDtoValidator();
        IValidator<SourceFormatNode> nodeValidator = new SourceFormatNodeValidator();
        ISourceFormatNodeMappers sourceFormatNodeMappers = new SourceFormatNodeMappers();
        ISourceFormatMappers mappers = new SourceFormatMappers(sourceFormatNodeMappers);
        DbContextOptions<SourceFormatNodeDbContext> dbContextOptions =
            new DbContextOptionsBuilder<SourceFormatNodeDbContext>()
                .UseSqlite(connection).LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        SourceFormatNodeDbContext ctx = new SourceFormatNodeDbContext(dbContextOptions);
        ctx.Database.EnsureCreated();
        ISourceFormatsNodeRepository sourceFormatsNodeRepository = new SourceFormatNodeRepository(
            ctx, nodeValidator, new GuardService());
        ISourceFormatNodeCacheService sourceFormatNodeCacheService = new SourceFormatNodeCacheService();
        ISourceFormatNodeService sourceFormatNodeService =
            new SourceFormats.SourceFormatsService.SourceFormatNodeService.SourceFormatNodeService(
                validator, new GuardService(), mappers, sourceFormatsNodeRepository, sourceFormatNodeCacheService);
        _sourceFormatsService = new SourceFormatsService(sourceFormatNodeService);
    }
}