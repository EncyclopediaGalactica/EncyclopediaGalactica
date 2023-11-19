namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
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
        List<DocumentGraphqlInput> result = await Sut.DocumentService.GetAllAsync();

        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task ReturnAll_WhenThereAreItemsInTheDatabase()
    {
        // Arrange
        List<long> recorded = await CreateDocumentDtoTestData(3);

        // Act
        List<DocumentGraphqlInput> result = await Sut.DocumentService.GetAllAsync();

        // Assert
        result.Count.Should().Be(3);
    }
}