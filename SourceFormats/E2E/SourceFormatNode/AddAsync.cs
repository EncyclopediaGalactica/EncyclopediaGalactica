namespace EncyclopediaGalactica.SourceFormats.E2E;

using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;
using SourceFormatsService.Interfaces;
using Xunit;

public partial class SourceFormatNodeSdk_Should
{
    [Fact]
    public async Task Return_201_WhenCreatingNewSourceFormatNode_AndReturnResult()
    {
        // Arrange
        string name = "asd";
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName(name)
            .Build();

        // Act
        SourceFormatNodeAddResponseModel responseModel = await SourceFormatsSdk.SourceFormatNode.AddAsync(requestModel)
            .ConfigureAwait(false);

        // Assert
        responseModel.Should().NotBeNull();
        responseModel.IsOperationSuccessful.Should().BeTrue();
        responseModel.Message.Should().Be(SourceFormatsServiceResultStatuses.Success);
        responseModel.Result.Should().NotBeNull();
        responseModel.Result.Should().BeOfType<SourceFormatNodeDto>();
        responseModel.Result.Id.Should().BeGreaterThan(0);
        responseModel.Result.Name.Should().Be(name);
    }

    [Fact]
    public async Task Return_400_WhenSourceFormatNodeNameUniquenessIsViolated_ByAdding()
    {
        // Arrange
        string name = "asd";
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName(name)
            .Build();
        SourceFormatNodeAddResponseModel responseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(requestModel)
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeAddResponseModel responseModel2 = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(requestModel)
            .ConfigureAwait(false);

        // Assert
        responseModel2.Should().NotBeNull();
        responseModel2.IsOperationSuccessful.Should().BeFalse();
    }
}