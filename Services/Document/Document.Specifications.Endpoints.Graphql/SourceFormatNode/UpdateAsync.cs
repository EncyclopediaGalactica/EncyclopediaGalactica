namespace EncyclopediaGalactica.Services.Document.Specifications.Endpoints.Graphql.SourceFormatNode;

using System.Net;
using System.Threading.Tasks;
using Contracts.Input;
using FluentAssertions;
using Sdk.Client.Models.SourceFormatNode;
using Service.Interfaces;
using Xunit;

[Trait("Category", "DocumentService")]
public partial class SourceFormatNodeSdk_Should
{
    [Fact]
    public async Task Return_200_AndTheResult_WhenUpdating()
    {
        // Arrange
        string updatedName = "bbsd";
        SourceFormatNodeAddRequestModel addRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("asd")
            .Build();
        SourceFormatNodeAddResponseModel addResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(addRequestModel);

        // Act
        SourceFormatNodeUpdateRequestModel updateRequestModel = new SourceFormatNodeUpdateRequestModel.Builder()
            .SetId(addResponseModel.Result.Id)
            .SetName(updatedName)
            .Build();
        SourceFormatNodeUpdateResponseModel updateResponseModel = await SourceFormatsSdk.SourceFormatNode
            .UpdateAsync(updateRequestModel);

        // Assert
        updateResponseModel.Should().NotBeNull();
        updateResponseModel.IsOperationSuccessful.Should().BeTrue();
        updateResponseModel.Result.Should().NotBeNull();
        updateResponseModel.Result.Should().BeOfType<SourceFormatNodeInputContract>();
        updateResponseModel.Result.Id.Should().Be(addResponseModel.Result.Id);
        updateResponseModel.Result.Name.Should().Be(updatedName);
    }

    [Fact]
    public async Task Return_404_AndNullResult_WhenThereIsNoSuchEntityToBeUpdated()
    {
        // Arrange
        string updatedName = "asd";
        SourceFormatNodeAddRequestModel addRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("bbsd")
            .Build();
        SourceFormatNodeAddResponseModel addResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(addRequestModel);

        // Act
        addResponseModel.Should().NotBeNull();
        addResponseModel.Result.Should().NotBeNull();

        SourceFormatNodeUpdateRequestModel updateRequestModel = new SourceFormatNodeUpdateRequestModel.Builder()
            .SetId(addResponseModel.Result.Id + 100)
            .SetName(updatedName)
            .Build();
        SourceFormatNodeUpdateResponseModel updateResponseModel = await SourceFormatsSdk.SourceFormatNode
            .UpdateAsync(updateRequestModel);

        // Assert
        updateResponseModel.Should().NotBeNull();
        updateResponseModel.Message
            .Substring(1, updateResponseModel.Message.Length - 2)
            .Should().Be(SourceFormatsServiceResultStatuses.NoSuchEntity);
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
            .AddAsync(baseRequestModel);

        SourceFormatNodeAddRequestModel addRequestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("asdff")
            .Build();
        SourceFormatNodeAddResponseModel addResponseModel = await SourceFormatsSdk.SourceFormatNode
            .AddAsync(addRequestModel);

        // Act
        SourceFormatNodeUpdateRequestModel updateRequestModel = new SourceFormatNodeUpdateRequestModel.Builder()
            .SetId(addResponseModel.Result.Id)
            .SetName(updatedName)
            .Build();
        SourceFormatNodeUpdateResponseModel updateResponseModel = await SourceFormatsSdk.SourceFormatNode
            .UpdateAsync(updateRequestModel);

        // Assert
        updateResponseModel.Should().NotBeNull();
        updateResponseModel.Message.Substring(1, updateResponseModel.Message.Length - 2)
            .Should().Be(SourceFormatsServiceResultStatuses.ValidationError);
        updateResponseModel.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        updateResponseModel.IsOperationSuccessful.Should().BeFalse();
        updateResponseModel.Result.Should().BeNull();
    }
}