namespace EncyclopediaGalactica.SourceFormats.E2E;

using System;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using QA.Datasets;
using Sdk.Models;
using Sdk.Models.SourceFormatNode;
using SourceFormatsService.Interfaces;
using Xunit;

public partial class SourceFormatNodeSdk_Should
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.AddValidationDataSet), MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_WhenTheUserTriesToBuildAnInvalidDataset(string name)
    {
        // Arrange & Act
        Action action = () =>
        {
            SourceFormatNodeAddRequestModel model = new SourceFormatNodeAddRequestModel.Builder()
                .SetName(name)
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }

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