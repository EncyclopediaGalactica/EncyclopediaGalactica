namespace EncyclopediaGalactica.SourceFormats.E2E;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using QA.Datasets;
using Sdk.Models;
using Sdk.Models.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeSdk_Should : TestBase
{
    public SourceFormatNodeSdk_Should(SourceFormatWebApplicationFactory<Program> webApplicationFactory) : base(
        webApplicationFactory)
    {
    }

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
        responseModel.HttpStatusCode.Should().Be(201);
        responseModel.Message.Should().BeNull();
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
        responseModel2.HttpStatusCode.Should().Be(400);
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
        updateResponseModel.Should().BeNull();
        updateResponseModel.Message.Should().BeNull();
        updateResponseModel.HttpStatusCode.Should().Be(204);
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
        SourceFormatNodeUpdateRequestModel updateRequestModel = new SourceFormatNodeUpdateRequestModel.Builder()
            .SetId(addResponseModel.Result.Id + 100)
            .SetName(updatedName)
            .Build();
        SourceFormatNodeUpdateResponseModel updateResponseModel = await SourceFormatsSdk.SourceFormatNode
            .UpdateAsync(updateRequestModel)
            .ConfigureAwait(false);

        // Assert
        updateResponseModel.Should().BeNull();
        updateResponseModel.Message.Should().BeNull();
        updateResponseModel.HttpStatusCode.Should().Be(204);
        updateResponseModel.IsOperationSuccessful.Should().BeTrue();
        updateResponseModel.Result.Should().BeNull();
    }

    [Fact]
    public async Task Return_400_WhenSourceFormatNodeNameUniquenessIsViolated_ByUpdate()
    {
        // Arrange
        string updatedName = "asd";
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
        updateResponseModel.Should().BeNull();
        updateResponseModel.Message.Should().NotBeNull();
        updateResponseModel.HttpStatusCode.Should().Be(400);
        updateResponseModel.IsOperationSuccessful.Should().BeFalse();
        updateResponseModel.Result.Should().BeNull();
    }
}