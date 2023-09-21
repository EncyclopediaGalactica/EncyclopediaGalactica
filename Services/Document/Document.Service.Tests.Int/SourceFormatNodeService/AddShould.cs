namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class AddShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_SuccessCode_AndWithOperationResult()
    {
        // Arrange
        string name = "asd";
        SourceFormatNodeDto dto = new()
        {
            Name = name
        };

        // Act
        SourceFormatNodeDto result = await Sut
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeNull();
        result.Id.Should().NotBe(0);
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(name);
    }
}