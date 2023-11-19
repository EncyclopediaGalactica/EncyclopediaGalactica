namespace EncyclopediaGalactica.Services.Document.Specifications.Endpoints.Graphql.SourceFormatNode;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Input;
using FluentAssertions;
using Sdk.Client.Models;
using Sdk.Client.Models.SourceFormatNode;
using Service.Interfaces;
using Xunit;

[Trait("Category", "DocumentService")]
public partial class SourceFormatNodeSdk_Should
{
    [Fact]
    public void Throw_WhenTheUserTriesToBuildRequestModel_WithoutId()
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeDeleteRequestModel requestModel = new SourceFormatNodeDeleteRequestModel.Builder()
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }

    [Fact]
    public async Task Return_201_WhenEntityIsDeleted_AndReturnOperationDetails()
    {
        // Arrange
        SourceFormatNodeAddRequestModel addRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("asd")
            .Build();
        SourceFormatNodeAddResponseModel addResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(addRequestModel);

        // Act
        SourceFormatNodeDeleteRequestModel deleteRequestModel = new SourceFormatNodeDeleteRequestModel.Builder()
            .SetId(addResponseModel.Result.Id)
            .Build();
        SourceFormatNodeDeleteResponseModel deleteResponseModel = await SourceFormatsSdk.SourceFormatNode
            .DeleteAsync(deleteRequestModel);

        // Assert
        deleteResponseModel.Should().NotBeNull();
        deleteResponseModel.Result.Should().BeNull();
        deleteResponseModel.IsOperationSuccessful.Should().BeTrue();

        SourceFormatNodeGetAllRequestModel getAllRequestModel = new SourceFormatNodeGetAllRequestModel.Builder()
            .Build();
        SourceFormatNodeGetAllResponseModel getAllResponseModel = await SourceFormatsSdk.SourceFormatNode
            .GetAllAsync(getAllRequestModel);
        getAllResponseModel.Should().NotBeNull();
        getAllResponseModel.Result.Should().NotBeNull();
        getAllResponseModel.Result.Should().BeOfType<List<SourceFormatNodeInputContract>>();
        getAllResponseModel.Result.Count.Should().Be(0);
    }

    [Fact]
    public async Task Return_404_WhenEntityDoesNotExistInTheDatabase()
    {
        // Act
        SourceFormatNodeDeleteRequestModel deleteRequestModel = new SourceFormatNodeDeleteRequestModel.Builder()
            .SetId(100)
            .Build();
        SourceFormatNodeDeleteResponseModel deleteResponseModel = await SourceFormatsSdk.SourceFormatNode
            .DeleteAsync(deleteRequestModel);

        // Assert
        deleteResponseModel.Should().NotBeNull();
        deleteResponseModel.Message
            .Substring(1, deleteResponseModel.Message.Length - 2)
            .Should().Be(SourceFormatsServiceResultStatuses.NoSuchEntity);
        deleteResponseModel.Result.Should().BeNull();
        deleteResponseModel.IsOperationSuccessful.Should().BeFalse();
    }
}