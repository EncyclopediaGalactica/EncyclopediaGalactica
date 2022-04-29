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
    public async Task Update_Entity()
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
}