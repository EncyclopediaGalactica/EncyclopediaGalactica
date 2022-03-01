namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests;

using System.Diagnostics;
using Ctx;
using Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repository.SourceFormatNode;
using ValidatorService;

public class BaseTest
{
    protected readonly ISourceFormatsNodeRepository _sut;

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
        _sut = new SourceFormatNodeRepository(ctx, new SourceFormatNodeValidator());
    }
}