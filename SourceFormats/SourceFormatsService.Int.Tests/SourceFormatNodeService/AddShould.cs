namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Interfaces;
using Interfaces.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
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
        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(SourceFormatsResultStatuses.SUCCESS);
        result.IsOperationSuccessful.Should().BeTrue();
        result.Result.Should().NotBeNull();
        result.Result.Id.Should().NotBe(0);
        result.Result.Id.Should().BeGreaterThan(0);
        result.Result.Name.Should().Be(name);
    }
}