namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Interfaces;
using Interfaces.SourceFormatNode;
using QA.Datasets;
using Xunit;

public class UpdateValidationShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_ValidationErrorCode_WhenInputIsNull()
    {
        // Act
        SourceFormatNodeSingleResultResponseModel responseModel = await _sourceFormatsService.SourceFormatNode
            .UpdateSourceFormatNodeAsync(null)
            .ConfigureAwait(false);

        // Assert
        responseModel.Message.Should().NotBeNull();
        responseModel.Result.Should().BeNull();
        responseModel.IsOperationSuccessful.Should().BeFalse();
        responseModel.Status.Should().Be(SourceFormatsResultStatuses.VALIDATION_ERROR);
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.UpdateValidationDataSet),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task ReturnsResponseModel_ValidationErrorCode_WhenInputIsInvalid(int id, string name)
    {
        // Act
        SourceFormatNodeDto dto = new()
        {
            Id = id,
            Name = name
        };
        SourceFormatNodeSingleResultResponseModel responseModel = await _sourceFormatsService.SourceFormatNode
            .UpdateSourceFormatNodeAsync(null)
            .ConfigureAwait(false);

        // Assert
        responseModel.Should().NotBeNull();
        responseModel.Result.Should().BeNull();
        responseModel.Status.Should().Be(SourceFormatsResultStatuses.VALIDATION_ERROR);
        responseModel.IsOperationSuccessful.Should().BeFalse();
    }
}