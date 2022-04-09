namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Linq;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;
using Xunit;

public class GetAllShould : BaseTest
{
    [Fact]
    public async Task ReturnAll()
    {
        // Arrange
        string name = "asdasd";
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName(name)
            .Build();

        await _sourceFormatsService.SourceFormatNode
            .AddAsync(requestModel)
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
    public async Task ReturnEmptyList_WhenNoElemInTheDatabase()
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