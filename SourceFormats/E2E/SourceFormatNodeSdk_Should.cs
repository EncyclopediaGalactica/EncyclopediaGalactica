namespace EncyclopediaGalactica.SourceFormats.E2E;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using QA.Datasets;
using Sdk.Models;
using Sdk.Models.SourceFormatNode;
using SourceFormatsService.Interfaces;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("E2E")]
public class SourceFormatNodeSdk_Should : TestBase
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

    [Fact]
    public async Task Return_200_AndTheResult_WhenUpdating()
    {
        // Arrange
        string updatedName = "bbsd";
        SourceFormatNodeAddRequestModel addRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("asd")
            .Build();
        SourceFormatNodeAddResponseModel addResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(addRequestModel)
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeUpdateRequestModel updateRequestModel = new SourceFormatNodeUpdateRequestModel.Builder()
            .SetId(addResponseModel.Result.Id)
            .SetName(updatedName)
            .Build();
        SourceFormatNodeUpdateResponseModel updateResponseModel = await SourceFormatsSdk.SourceFormatNode
            .UpdateAsync(updateRequestModel)
            .ConfigureAwait(false);

        // Assert
        updateResponseModel.Should().NotBeNull();
        updateResponseModel.Message.Should().Be(SourceFormatsServiceResultStatuses.Success);
        updateResponseModel.IsOperationSuccessful.Should().BeTrue();
        updateResponseModel.Result.Should().NotBeNull();
        updateResponseModel.Result.Should().BeOfType<SourceFormatNodeDto>();
        updateResponseModel.Result.Id.Should().Be(addResponseModel.Result.Id);
        updateResponseModel.Result.Name.Should().Be(updatedName);
    }

    [Fact]
    public async Task Return_200_AndNullResult_WhenThereIsNoSuchEntityToBeUpdated()
    {
        // Arrange
        string updatedName = "asd";
        SourceFormatNodeAddRequestModel addRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("bbsd")
            .Build();
        SourceFormatNodeAddResponseModel addResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(addRequestModel)
            .ConfigureAwait(false);

        // Act
        if (addResponseModel is null) throw new Exception("addresponsemodel is null");

        if (addResponseModel.Result is null) throw new Exception("result is null");

        SourceFormatNodeUpdateRequestModel updateRequestModel = new SourceFormatNodeUpdateRequestModel.Builder()
            .SetId(addResponseModel.Result.Id + 100)
            .SetName(updatedName)
            .Build();
        SourceFormatNodeUpdateResponseModel updateResponseModel = await SourceFormatsSdk.SourceFormatNode
            .UpdateAsync(updateRequestModel)
            .ConfigureAwait(false);

        // Assert
        updateResponseModel.Should().NotBeNull();
        updateResponseModel.Message.Should().Be(SourceFormatsServiceResultStatuses.NoSuchEntity);
        updateResponseModel.IsOperationSuccessful.Should().BeFalse();
        updateResponseModel.Result.Should().BeNull();
    }

    [Fact]
    public async Task Return_400_WhenSourceFormatNodeNameUniquenessIsViolated_ByUpdate()
    {
        // Arrange
        string updatedName = "asd";
        SourceFormatNodeAddRequestModel baseRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("asd")
            .Build();
        SourceFormatNodeAddResponseModel baseResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(baseRequestModel)
            .ConfigureAwait(false);

        SourceFormatNodeAddRequestModel addRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("asdff")
            .Build();
        SourceFormatNodeAddResponseModel addResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(addRequestModel)
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeUpdateRequestModel updateRequestModel = new SourceFormatNodeUpdateRequestModel.Builder()
            .SetId(addResponseModel.Result.Id)
            .SetName(updatedName)
            .Build();
        SourceFormatNodeUpdateResponseModel updateResponseModel = await SourceFormatsSdk.SourceFormatNode
            .UpdateAsync(updateRequestModel)
            .ConfigureAwait(false);

        // Assert
        updateResponseModel.Should().NotBeNull();
        updateResponseModel.Message.Should().NotBe(SourceFormatsServiceResultStatuses.ValidationError);
        updateResponseModel.IsOperationSuccessful.Should().BeFalse();
        updateResponseModel.Result.Should().BeNull();
    }
}