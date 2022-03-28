namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;

[ExcludeFromCodeCoverage]
public class AddShould : BaseTest
{
    public async Task Add_AnItem_AndReturnIt()
    {
        // Arrange
        SourceFormatNodeDto dto = new SourceFormatNodeDto()
        {
            Name = "asdasd"
        };

        // Act
        SourceFormatNodeDto result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Assert
        result.Id.Should().NotBe(0);
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(dto.Name);
    }
}