namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repository.SourceFormatNode;
using ValidatorService;

[ExcludeFromCodeCoverage]
public class BaseTest
{
#pragma warning disable CA1051
    protected readonly ISourceFormatsNodeRepository Sut;
#pragma warning restore CA1051

    public BaseTest()
    {
        SqliteConnection connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        DbContextOptions<SourceFormatNodeDbContext> sourceFormatNodeDbContextOptions = new
                DbContextOptionsBuilder<SourceFormatNodeDbContext>()
            .UseSqlite(connection)
            .LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging().EnableDetailedErrors()
            .Options;
        SourceFormatNodeDbContext ctx = new SourceFormatNodeDbContext(sourceFormatNodeDbContextOptions);
        ctx.Database.EnsureCreated();
        Sut = new SourceFormatNodeRepository(ctx, new SourceFormatNodeValidator());
    }
}