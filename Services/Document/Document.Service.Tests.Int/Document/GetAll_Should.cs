namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Output;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetAllShould : BaseTest
{
    [Fact]
    public async Task ReturnEmptyList_WhenNoItemInTheDatabase()
    {
        // Act
        List<DocumentResult> result = await Sut.DocumentService.GetAllAsync();

        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task ReturnAll_WhenThereAreItemsInTheDatabase()
    {
        // Arrange
        List<long> recorded = await CreateDocumentDtoTestData(3);

        // Act
        List<DocumentResult> result = await Sut.DocumentService.GetAllAsync();

        // Assert
        result.Count.Should().Be(3);
    }
}