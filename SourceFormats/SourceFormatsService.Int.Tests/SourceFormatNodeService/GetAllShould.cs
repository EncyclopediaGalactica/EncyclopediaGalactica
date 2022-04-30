namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class GetAllShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_SuccessCode_AllEntities_WhenThereAreEntitiesInTheDb()
    {
        // Arrange
        string name = "asdasd";
        SourceFormatNodeDto dto = new()
        {
            Name = name
        };

        await _sourceFormatsService.SourceFormatNode
            .AddAsync(dto)
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeGetAllResponseModel resultList = await _sourceFormatsService.SourceFormatNode
            .GetAllAsync()
            .ConfigureAwait(false);

        // Assert
        resultList.Result.Should().NotBeNull();
        resultList.Result.Should().NotBeEmpty();
        resultList.Result.Count.Should().Be(1);
        SourceFormatNodeDto elem = resultList.Result.ElementAt(0);
        elem.Name.Should().Be(name);
    }

    [Fact]
    public async Task ReturnsResponseModel_SuccessCode_EmptyList_WhenThereAreNoEntitiesInTheDb()
    {
        // Act
        SourceFormatNodeGetAllResponseModel result = await _sourceFormatsService.SourceFormatNode.GetAllAsync()
            .ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().NotBeNull();
        result.Result.Should().BeEmpty();
    }
}