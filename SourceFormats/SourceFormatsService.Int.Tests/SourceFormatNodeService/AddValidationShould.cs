namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using QA.Datasets;
using Sdk.Models.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddValidationShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_ValidationErrorCode_AndErrorDetails_WhenInputIsNull()
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

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.AddValidationDataSet), MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task ReturnsResponseModel_ValidationErrorCode_AndErrorDetails_WhenInputIsInvalid(
        string name)
    {
        // Act
        SourceFormatNodeDto dto = new() { Name = name };
        SourceFormatNodeAddResponseModel result = await _sourceFormatsService.SourceFormatNode
            .AddAsync(dto)
            .ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.Result.Should().BeNull();
        result.HttpStatusCode.Should().Be(400);
        result.IsOperationSuccessful.Should().BeFalse();
    }

    [Fact]
    public async Task ReturnsResponseModel_NotUniqueNameErrorCode_AndErrorDetails_WhenNameUniquenessIsViolated()
    {
        // Arrange
        string name = "asdasd";
        SourceFormatNodeDto dto = new()
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