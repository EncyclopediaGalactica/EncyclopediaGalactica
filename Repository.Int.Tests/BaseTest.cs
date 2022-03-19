namespace EncyclopediaGalactica.SourceFormats.Repository.Int.Tests;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
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

    protected async Task<(
        int childCount,
        long childId,
        long parentId,
        long rootNodeId)> PrepareSourceFormatNodeRepoWith_OneParentAnd_OneChild()
    {
        Entities.SourceFormatNode parent = await Sut.AddAsync(
            new Entities.SourceFormatNode("parent")).ConfigureAwait(false);
        Entities.SourceFormatNode child = await Sut.AddAsync(
            new Entities.SourceFormatNode("child1")).ConfigureAwait(false);

        Entities.SourceFormatNode result = await Sut.AddChildNodeAsync(
            child.Id,
            parent.Id,
            parent.Id).ConfigureAwait(false);
        (int, long, long, long) res = (
            1,
            child.Id,
            parent.Id,
            parent.Id);
        return res;
    }
}