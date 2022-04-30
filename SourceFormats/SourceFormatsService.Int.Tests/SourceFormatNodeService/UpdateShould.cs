namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class UpdateShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_SuccessCode_AndUpdatedEntity()
    {
        // Arrange
        SourceFormatNodeDto dto = new()
        {
            Name = "asd"
        };
        SourceFormatNodeAddResponseModel addResponseModel = await _sourceFormatsService.SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);
        string updatedName = "asdasd";
        SourceFormatNodeDto updateTemplate = new()
        {
            Id = addResponseModel.Result.Id,
            Name = updatedName
        };

        // Act
        SourceFormatNodeUpdateResponseModel updateResponseModel = await _sourceFormatsService.SourceFormatNode
            .UpdateSourceFormatNodeAsync(updateTemplate).ConfigureAwait(false);

        // Assert
        updateResponseModel.Message.Should().BeNull();
        updateResponseModel.IsOperationSuccessful.Should().BeTrue();
        updateResponseModel.Result.Id.Should().Be(updateTemplate.Id);
        updateResponseModel.Result.Name.Should().Be(updateTemplate.Name);
    }

    [Fact]
    public async Task ReturnsResponseModel_NoSuchEntityErrorCode_AndErrorDetails_WhenNoSuchEntityToBeUpdated()
    {
        SourceFormatNodeDto updateTemplate = new()
        {
            Id = 204,
            Name = "asdasd"
        };

        // Act
        SourceFormatNodeUpdateResponseModel updateResponseModel = await _sourceFormatsService.SourceFormatNode
            .UpdateSourceFormatNodeAsync(updateTemplate).ConfigureAwait(false);

        // Assert
        updateResponseModel.Message.Should().NotBeNullOrEmpty();
        updateResponseModel.Message.Should().NotBeNullOrWhiteSpace();
        updateResponseModel.IsOperationSuccessful.Should().BeFalse();
    }
}