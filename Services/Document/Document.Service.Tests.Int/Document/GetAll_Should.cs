namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class GetAll_Should : BaseTest
{
    [Fact]
    public async Task ReturnEmptyList_WhenNoItemInTheDatabase()
    {
        // Act
        List<DocumentDto> result = await Sut.DocumentService.GetAllAsync().ConfigureAwait(false);

        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task ReturnAll_WhenThereAreItemsInTheDatabase()
    {
        // Arrange
        List<long> recorded = await CreateDocumentDtoTestData(3);

        // Act
        List<DocumentDto> result = await Sut.DocumentService.GetAllAsync().ConfigureAwait(false);

        // Assert
        result.Count.Should().Be(3);
    }
}