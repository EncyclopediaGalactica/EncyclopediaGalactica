namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Ctx;
using Entities;
using Exceptions;
using FluentAssertions;
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
using SourceFormats.SourceFormatsService.SourceFormatNodeService;
using SourceFormatsCacheService;
using SourceFormatsCacheService.Interfaces;
using SourceFormatsCacheService.SourceFormatNode;
using ValidatorService;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddValidationShould
{
    [Fact]
    public async Task Throw_WhenInputIsInvalid()
    {
        // Arrange
        SqliteConnection connection = new SqliteConnection("Filename=:memory:");
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
        ISourceFormatsNodeRepository sourceFormatsNodeRepository = new SourceFormatNodeRepository(
            ctx, nodeValidator, new GuardService());
        ISourceFormatNodeCacheService sourceFormatNodeCacheService = new SourceFormatNodeCacheService();
        ISourceFormatsCacheService sourceFormatsCacheService =
            new SourceFormatsCacheService(sourceFormatNodeCacheService);
        ISourceFormatNodeService sourceFormatNodeService = new SourceFormatNodeService(
            validator, new GuardService(), mappers, sourceFormatsNodeRepository, sourceFormatNodeCacheService);

        // Act
        Func<Task> task = async () => { await sourceFormatNodeService.AddAsync(null!).ConfigureAwait(false); };

        // Assert
        await task.Should().ThrowExactlyAsync<SourceFormatNodeServiceInputValidationException>().ConfigureAwait(false);
    }
}