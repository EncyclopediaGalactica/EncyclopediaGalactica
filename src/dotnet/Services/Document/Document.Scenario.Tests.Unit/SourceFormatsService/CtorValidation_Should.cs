namespace EncyclopediaGalactica.Services.Document.Scenario.Tests.Unit.SourceFormatsService;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class CtorValidationShould
{
    // public static IEnumerable<object[]> Throw_WhenAnyCtorParamIsNull_Data => new List<object[]>
    // {
    //     new[]
    //     {
    //         null,
    //         new DocumentService(new GuardsService(),
    //             new SourceFormatMappers(
    //                 new Mock<ISourceFormatNodeMappers>().Object,
    //                 new Mock<IDocumentMappers>().Object),
    //             new Mock<IDocumentsRepository>().Object)
    //     },
    //     new object[] { null!, null! },
    //     new[]
    //     {
    //         new SourceFormatNodeService(
    //             new SourceFormatNodeDtoValidator(),
    //             new GuardsService(),
    //             new SourceFormatMappers(
    //                 new Mock<ISourceFormatNodeMappers>().Object,
    //                 new Mock<IDocumentMappers>().Object),
    //             new SourceFormatNodeRepository(
    //                 new DbContextOptions<SourceFormatsDbContext>(),
    //                 new SourceFormatNodeValidator(),
    //                 new GuardsService()),
    //             new SourceFormatNodeCacheService(),
    //             new Mock<ILogger<SourceFormatNodeService>>().Object),
    //         null
    //     },
    // };

    // [Theory]
    // [MemberData(nameof(Throw_WhenAnyCtorParamIsNull_Data))]
    // public void Throw_WhenAnyCtorParamIsNull(
    //     ISourceFormatNodeService sourceFormatNodeService,
    //     IDocumentService documentService)
    // {
    //     // Arrange & Act
    //     Action action = () =>
    //     {
    //         ISourceFormatsService sourceFormatsService =
    //             new Services.Document.SourceFormatsService.SourceFormatsService(
    //                 sourceFormatNodeService,
    //                 documentService);
    //     };
    //
    //     // Assert
    //     action.Should().Throw<ArgumentNullException>();
    // }
}