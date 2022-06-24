namespace EncyclopediaGalactica.SourceFormats.E2E.SourceFormatNode;

using System;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;
using SourceFormatsService.Interfaces;
using Xunit;

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
    public async Task Return_404_AndNullResult_WhenThereIsNoSuchEntityToBeUpdated()
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
        if (addResponseModel is null)
        {
            throw new Exception("addresponsemodel is null");
        }

        if (addResponseModel.Result is null)
        {
            throw new Exception("result is null");
        }

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