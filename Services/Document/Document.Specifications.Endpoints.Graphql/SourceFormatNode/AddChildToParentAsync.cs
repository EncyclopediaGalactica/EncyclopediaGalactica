namespace EncyclopediaGalactica.Services.Document.Specifications.Endpoints.Graphql.SourceFormatNode;

using System;
using System.Threading.Tasks;
using Contracts.Input;
using FluentAssertions;
using Sdk.Client.Models;
using Sdk.Client.Models.SourceFormatNode;
using Service.Interfaces;
using Tests.Datasets;
using Xunit;

[Trait("Category", "DocumentService")]
public partial class SourceFormatNodeSdk_Should
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.SDKModels_AddChildToParent_InputValidation_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public void Throw_WhenTheUserTtriesToBuildRequestModel_BasedOnInvalidInput(
        long childId,
        long parentId)
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeAddChildToParentRequestModel requestModel = new
                    SourceFormatNodeAddChildToParentRequestModel.Builder()
                .SetChildrenNodeId(childId)
                .SetParentNodeId(parentId)
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }

    public async Task Return_201_AndAddChildToParent()
    {
        // Arrange
        SourceFormatNodeAddRequestModel rootNodeAddRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("root")
            .Build();
        SourceFormatNodeAddResponseModel rootNodeResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(rootNodeAddRequestModel);

        SourceFormatNodeAddRequestModel parentNodeAddRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("parent")
            .Build();
        SourceFormatNodeAddResponseModel parentNodeResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(parentNodeAddRequestModel);
        SourceFormatNodeAddChildToParentRequestModel addParentToRootNodeRequestModel = new
                SourceFormatNodeAddChildToParentRequestModel.Builder()
            .SetChildrenNodeId(parentNodeResponseModel.Result!.Id)
            .SetParentNodeId(rootNodeResponseModel.Result!.Id)
            .Build();
        SourceFormatNodeAddChildToParentResponseModel addParentToRootNodeResponseModel = await SourceFormatsSdk
            .SourceFormatNode.AddChildToParentAsync(addParentToRootNodeRequestModel).ConfigureAwait(false);

        SourceFormatNodeAddRequestModel childRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("child")
            .Build();
        SourceFormatNodeAddResponseModel childResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(childRequestModel);

        // Act
        SourceFormatNodeAddChildToParentRequestModel addChildToParentResponseModel = new
                SourceFormatNodeAddChildToParentRequestModel.Builder()
            .SetChildrenNodeId(childResponseModel.Result!.Id)
            .SetParentNodeId(addParentToRootNodeResponseModel.Result!.Id)
            .Build();
        SourceFormatNodeAddChildToParentResponseModel responseModel = await SourceFormatsSdk.SourceFormatNode
            .AddChildToParentAsync(addChildToParentResponseModel);

        // Assert
        responseModel.Should().NotBeNull();
        responseModel.Should().BeOfType<SourceFormatNodeAddChildToParentResponseModel>();
        responseModel.Result.Should().NotBeNull();
        responseModel.Result.Should().BeOfType<SourceFormatNodeInputContract>();
        responseModel.Result?.Id.Should().Be(childResponseModel.Result.Id);
        responseModel.IsOperationSuccessful.Should().BeTrue();
        responseModel.Message.Should().NotBeNull();
        responseModel.Message.Should().Be(SourceFormatsServiceResultStatuses.Success);
    }
}