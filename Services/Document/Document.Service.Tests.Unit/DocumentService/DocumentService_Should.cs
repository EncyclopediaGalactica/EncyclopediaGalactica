namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Unit.DocumentService;

using System.Diagnostics.CodeAnalysis;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class DocumentService_Should
{
    // public static IEnumerable<object[]> ThrowArgumentNullException_WhenInjected_IsNull_Data = new List<object[]>
    // {
    //     new object[]
    //     {
    //         null,
    //         new SourceFormatMappers(
    //             new Mock<ISourceFormatNodeMappers>().Object,
    //             new Mock<IDocumentMappers>().Object),
    //         new DocumentRepository(
    //             new DbContextOptions<SourceFormatsDbContext>(),
    //             new Mock<IValidator<Document>>().Object)
    //     },
    //     new object[]
    //     {
    //         new GuardsService(),
    //         null,
    //         new DocumentRepository(
    //             new DbContextOptions<SourceFormatsDbContext>(),
    //             new Mock<IValidator<Document>>().Object)
    //     },
    //     new object[]
    //     {
    //         new GuardsService(),
    //         new SourceFormatMappers(
    //             new Mock<ISourceFormatNodeMappers>().Object,
    //             new Mock<IDocumentMappers>().Object),
    //         null
    //     }
    // };
    //
    // [Theory]
    // [MemberData(nameof(ThrowArgumentNullException_WhenInjected_IsNull_Data))]
    // public void ThrowArgumentNullException_WhenInjected_IsNull(
    //     IGuardsService guardsService,
    //     ISourceFormatMappers mappers,
    //     IDocumentsRepository documentsRepository)
    // {
    //     // Arrange && Act
    //     Action action = () =>
    //     {
    //         new DocumentService(
    //             guardsService,
    //             mappers,
    //             documentsRepository);
    //     };
    //
    //     // Assert
    //     action.Should().ThrowExactly<ArgumentNullException>();
    // }
}