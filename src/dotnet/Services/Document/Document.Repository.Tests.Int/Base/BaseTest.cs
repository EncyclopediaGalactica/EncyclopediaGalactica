namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Base;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repository.Document;
using ValidatorService;

[ExcludeFromCodeCoverage]
public partial class BaseTest
{
#pragma warning disable CA1051
    protected readonly ISourceFormatsRepository Sut;
#pragma warning restore CA1051

    public BaseTest()
    {
        SqliteConnection connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        DbContextOptions<DocumentDbContext> sourceFormatsDbContextOptions = new
                DbContextOptionsBuilder<DocumentDbContext>()
            .UseSqlite(connection)
            .LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging().EnableDetailedErrors()
            .Options;
        DocumentDbContext ctx = new DocumentDbContext(sourceFormatsDbContextOptions);
        ctx.Database.EnsureCreated();

        IDocumentsRepository documentsRepository = new DocumentRepository(
            sourceFormatsDbContextOptions,
            new DocumentValidator());
        Sut = new SourceFormatsRepository(
            documentsRepository);
    }
}