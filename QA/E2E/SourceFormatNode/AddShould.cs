namespace E2E.SourceFormatNode;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.SourceFormats.Dtos;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddShould : TestBase
{
    public AddShould(SourceFormatWebApplicationFactory<Program> webApplicationFactory) : base(webApplicationFactory)
    {
    }

    [Fact]
    public async Task Add()
    {
        // Arrange
        SourceFormatNodeDto newDto = new SourceFormatNodeDto
        {
            Name = "asdd"
        };

        // Act
        SourceFormatNodeDto? saved = await _sourceFormatsSdk.SourceFormatNode.AddAsync(newDto)
            .ConfigureAwait(false);

        // Assert
        saved.Should().NotBeNull();
        saved.Name.Should().Be(newDto.Name);
        saved.Id.Should().BeGreaterThan(0);
    }
}