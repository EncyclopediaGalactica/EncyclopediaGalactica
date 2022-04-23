namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddValidationShould : BaseTest
{
    [Fact]
    public async Task ReturnModel_WithOperationDetails_WhenInputIsNull()
    {
        // Act
        SourceFormatNodeAddResponseModel result = await _sourceFormatsService.SourceFormatNode
            .AddAsync(null!)
            .ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.Result.Should().BeNull();
        result.HttpStatusCode.Should().Be(400);
        result.IsOperationSuccessful.Should().BeFalse();
    }

    [Fact]
    public async Task ReturnResponseModel_WithOperationDetails_WhenNodeNameUniquenessIsViolated()
    {
        // Arrange
        string name = "asdasd";
        SourceFormatNodeDto dto = new SourceFormatNodeDto
        {
            Name = name
        };

        await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Act
        SourceFormatNodeAddResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.HttpStatusCode.Should().Be(400);
        result.IsOperationSuccessful.Should().BeFalse();
    }
}