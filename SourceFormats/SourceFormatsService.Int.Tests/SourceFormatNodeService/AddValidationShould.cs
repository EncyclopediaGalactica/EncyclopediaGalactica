namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Interfaces;
using Interfaces.SourceFormatNode;
using QA.Datasets;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class AddValidationShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_ValidationErrorCode_AndErrorDetails_WhenInputIsNull()
    {
        // Act
        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService.SourceFormatNode
            .AddAsync(null!)
            .ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.IsOperationSuccessful.Should().BeFalse();
        result.Result.Should().BeNull();
        result.Status.Should().Be(SourceFormatsServiceResultStatuses.ValidationError);
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.AddValidationDataSet), MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task ReturnsResponseModel_ValidationErrorCode_AndErrorDetails_WhenInputIsInvalid(
        string name)
    {
        // Act
        SourceFormatNodeDto dto = new() { Name = name };
        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService.SourceFormatNode
            .AddAsync(dto)
            .ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeNull();
        result.Status.Should().Be(SourceFormatsServiceResultStatuses.ValidationError);
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
        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(SourceFormatsServiceResultStatuses.ValidationError);
        result.IsOperationSuccessful.Should().BeFalse();
    }
}