namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Xunit;

public class GetAllShould : BaseTest
{
    [Fact]
    public async Task ReturnAll()
    {
        // Arrange
        SourceFormatNodeDto dto = new SourceFormatNodeDto
        {
            Name = "bla"
        };
        SourceFormatNodeDto result = await _sourceFormatsService.SourceFormatNode
            .AddAsync(dto)
            .ConfigureAwait(false);

        // Act
        List<SourceFormatNodeDto> resultList = await _sourceFormatsService.SourceFormatNode
            .GetAllAsync().ConfigureAwait(false);

        // Assert
        resultList.Should().NotBeEmpty();
        resultList.Count.Should().Be(1);
        SourceFormatNodeDto elem = resultList.ElementAt(0);
        elem.Name.Should().Be(dto.Name);
    }

    [Fact]
    public async Task ReturnEmptyList_WhenNoElemInTheDatabase()
    {
        // Act
        List<SourceFormatNodeDto> result = await _sourceFormatsService.SourceFormatNode.GetAllAsync()
            .ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}