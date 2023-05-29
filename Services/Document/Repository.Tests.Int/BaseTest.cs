namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

[ExcludeFromCodeCoverage]
public class BaseTest
{
#pragma warning disable CA1051
    protected readonly ISourceFormatsRepository Sut;
#pragma warning restore CA1051

    public BaseTest()
    {
        SqliteConnection connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        DbContextOptions<SourceFormatsDbContext> sourceFormatsDbContextOptions = new
                DbContextOptionsBuilder<SourceFormatsDbContext>()
            .UseSqlite(connection)
            .LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging().EnableDetailedErrors()
            .Options;
        SourceFormatsDbContext ctx = new SourceFormatsDbContext(sourceFormatsDbContextOptions);
        ctx.Database.EnsureCreated();

        ISourceFormatNodeRepository sourceFormatNodeRepository = new SourceFormatNodeRepository(
            sourceFormatsDbContextOptions,
            new SourceFormatNodeValidator(),
            new GuardsService());
        IDocumentsRepository documentsRepository = new DocumentRepository(
            sourceFormatsDbContextOptions,
            new DocumentValidator());
        Sut = new SourceFormatsRepository(
            sourceFormatNodeRepository,
            documentsRepository);
    }

    protected async Task<(
        int childCount,
        long childId,
        long parentId,
        long rootNodeId)> PrepareSourceFormatNodeRepoWith_OneParentAnd_OneChild()
    {
        Entities.SourceFormatNode parent = await Sut.SourceFormatNodes.AddAsync(
            new Entities.SourceFormatNode("parent")).ConfigureAwait(false);
        Entities.SourceFormatNode child = await Sut.SourceFormatNodes.AddAsync(
            new Entities.SourceFormatNode("child1")).ConfigureAwait(false);

        Entities.SourceFormatNode result = await Sut.SourceFormatNodes.AddChildNodeAsync(
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