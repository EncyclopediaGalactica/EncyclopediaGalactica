namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Interfaces;
using Interfaces.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class AddChildToParentShould : BaseTest
{
    [Fact]
    public async Task AddChildToParent_WhenParentIsAlreadyInTheTree_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeSingleResultResponseModel rootNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("rootnode"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel parentNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("parent"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel childNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("child"))
            .ConfigureAwait(false);

        SourceFormatNodeSingleResultResponseModel addParentToRoot = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(parentNode.Result!, rootNode.Result!)
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeSingleResultResponseModel addChildToParent = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(childNode.Result!, parentNode.Result!)
            .ConfigureAwait(false);

        // Assert
        addChildToParent.Should().BeOfType<SourceFormatNodeSingleResultResponseModel>();
        addChildToParent.Result.Should().NotBeNull();
        addChildToParent.Status.Should().Be(SourceFormatsServiceResultStatuses.Success);
        addChildToParent.IsOperationSuccessful.Should().BeTrue();

        addChildToParent.Result!.Id.Should().BeGreaterThan(0);
        addChildToParent.Result.Name.Should().Be("child");

        SourceFormatNodeSingleResultResponseModel childNodeDetails = await _sourceFormatsService.SourceFormatNode
            .GetByIdAsync(childNode.Result!.Id)
            .ConfigureAwait(false);
        childNodeDetails.Result!.ParentNodeId.Should().Be(parentNode.Result!.Id);
        childNodeDetails.Result!.RootNodeId.Should().Be(rootNode.Result!.Id);
        childNodeDetails.Result!.IsRootNode.Should().Be(0);
    }

    [Fact]
    public async Task AddChildToParent_WhenParentIdRootNode_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeSingleResultResponseModel rootNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("rootnode"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel parentNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("parent"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel childNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("child"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel childNode2 = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("child2"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel addParentToRoot = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(parentNode.Result!, rootNode.Result!)
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel addChildToParent = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(childNode.Result!, parentNode.Result!)
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeSingleResultResponseModel addChild2ToRoot = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(childNode2.Result!, rootNode.Result!)
            .ConfigureAwait(false);

        // Assert
        addChild2ToRoot.Should().BeOfType<SourceFormatNodeSingleResultResponseModel>();
        addChild2ToRoot.Result.Should().NotBeNull();
        addChild2ToRoot.Result.Should().BeOfType<SourceFormatNodeDto>();
        addChild2ToRoot.IsOperationSuccessful.Should().BeTrue();
        addChild2ToRoot.Status.Should().Be(SourceFormatsServiceResultStatuses.Success);

        SourceFormatNodeSingleResultResponseModel child2 = await _sourceFormatsService.SourceFormatNode
            .GetByIdAsync(childNode2.Result!.Id)
            .ConfigureAwait(false);
        child2.Should().NotBeNull();
        child2.Result!.ParentNodeId.Should().Be(rootNode.Result!.Id);
    }

    [Fact]
    public async Task AddChildToParent_WhenParentIsNotRootButShouldBe_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeSingleResultResponseModel parentNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("parent"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel childNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("child"))
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeSingleResultResponseModel childNodeAdded = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(childNode.Result!, parentNode.Result!)
            .ConfigureAwait(false);

        // Assert
        childNodeAdded.Should().NotBeNull();
        childNodeAdded.Result.Should().NotBeNull();
        childNodeAdded.Result.Should().BeOfType<SourceFormatNodeDto>();
        childNodeAdded.Result!.Id.Should().Be(childNode.Result!.Id);
        childNodeAdded.Result.Name!.Should().Be(childNode.Result!.Name);

        SourceFormatNodeSingleResultResponseModel childNodeDetails = await _sourceFormatsService.SourceFormatNode
            .GetByIdAsync(childNode.Result.Id)
            .ConfigureAwait(false);
        childNodeDetails.Result!.ParentNodeId.Should().Be(parentNode.Result!.Id);

        SourceFormatNodeSingleResultResponseModel parentNodeDetails = await _sourceFormatsService.SourceFormatNode
            .GetByIdAsync(parentNode.Result.Id)
            .ConfigureAwait(false);
        parentNodeDetails.Result!.IsRootNode.Should().Be(1);
    }

    [Fact]
    public async Task Return_ResponseModel_WithFailedOperation_AndNoSuchChildEntityMessage()
    {
        // Arrange
        SourceFormatNodeSingleResultResponseModel parentNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("parent"))
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeSingleResultResponseModel responseModel = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(new SourceFormatNodeDto { Id = 100 }, parentNode.Result!)
            .ConfigureAwait(false);

        // Assert
        responseModel.Should().NotBeNull();
        responseModel.Result.Should().BeNull();
        responseModel.IsOperationSuccessful.Should().BeFalse();
        responseModel.Status.Should().Be(SourceFormatsServiceResultStatuses.NoSuchEntity);
    }

    [Fact]
    public async Task Return_ResponseModel_WithFailedOperation_AndNoSuchParentEntityMessage()
    {
        // Arrange
        SourceFormatNodeSingleResultResponseModel childNode = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("parent"))
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeSingleResultResponseModel responseModel = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(childNode.Result!, new SourceFormatNodeDto { Id = 100 })
            .ConfigureAwait(false);

        // Assert
        responseModel.Should().NotBeNull();
        responseModel.Result.Should().BeNull();
        responseModel.IsOperationSuccessful.Should().BeFalse();
        responseModel.Status.Should().Be(SourceFormatsServiceResultStatuses.NoSuchEntity);
    }
}