namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Net;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using QA.Datasets;
using Sdk.Models.SourceFormatNode;
using Xunit;

public class UpdateValidationShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_ValidationErrorCode_WhenInputIsNull()
    {
        // Act
        SourceFormatNodeUpdateResponseModel responseModel = await _sourceFormatsService.SourceFormatNode
            .UpdateSourceFormatNodeAsync(null)
            .ConfigureAwait(false);

        // Assert
        responseModel.Message.Should().NotBeNull();
        responseModel.Result.Should().BeNull();
        responseModel.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        responseModel.IsOperationSuccessful.Should().BeFalse();
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
        SourceFormatNodeUpdateResponseModel responseModel = await _sourceFormatsService.SourceFormatNode
            .UpdateSourceFormatNodeAsync(null)
            .ConfigureAwait(false);

        // Assert
        responseModel.Message.Should().NotBeNull();
        responseModel.Result.Should().BeNull();
        responseModel.HttpStatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        responseModel.IsOperationSuccessful.Should().BeFalse();
    }
}