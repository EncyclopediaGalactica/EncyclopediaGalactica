namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class AddChildToParentShould : BaseTest
{
    [Fact]
    public async Task AddChildToParent_WhenParentIsAlreadyInTheTree_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeInputContract rootNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("rootnode"));
        SourceFormatNodeInputContract parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("parent"));
        SourceFormatNodeInputContract childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("child"));

        SourceFormatNodeInputContract addParentToRoot = await Sut.SourceFormatNode
            .AddChildToParentAsync(parentNode, rootNode);

        // Act
        SourceFormatNodeInputContract addChildToParent = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode, parentNode);

        // Assert
        addChildToParent.Should().BeOfType<SourceFormatNodeInputContract>();
        addChildToParent.Should().NotBeNull();
        addChildToParent.Id.Should().BeGreaterThan(0);
        addChildToParent.Name.Should().Be("child");

        SourceFormatNodeInputContract childNodeDetails = await Sut.SourceFormatNode
            .GetByIdAsync(childNode.Id);
        childNodeDetails.ParentNodeId.Should().Be(parentNode.Id);
        childNodeDetails.RootNodeId.Should().Be(rootNode.Id);
        childNodeDetails.IsRootNode.Should().Be(0);
    }

    [Fact]
    public async Task AddChildToParent_WhenParentIdRootNode_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeInputContract rootNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("rootnode"));
        SourceFormatNodeInputContract parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("parent"));
        SourceFormatNodeInputContract childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("child"));
        SourceFormatNodeInputContract childNode2 = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("child2"));
        SourceFormatNodeInputContract addParentToRoot = await Sut.SourceFormatNode
            .AddChildToParentAsync(parentNode, rootNode);
        SourceFormatNodeInputContract addChildToParent = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode, parentNode);

        // Act
        SourceFormatNodeInputContract addChild2ToRoot = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode2, rootNode);

        // Assert
        addChild2ToRoot.Should().BeOfType<SourceFormatNodeInputContract>();
        addChild2ToRoot.Should().NotBeNull();

        SourceFormatNodeInputContract child2 = await Sut.SourceFormatNode
            .GetByIdAsync(childNode2.Id);
        child2.Should().NotBeNull();
        child2.ParentNodeId.Should().Be(rootNode.Id);
    }

    [Fact]
    public async Task AddChildToParent_WhenParentIsNotRootButShouldBe_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeInputContract parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("parent"));
        SourceFormatNodeInputContract childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("child"));

        // Act
        SourceFormatNodeInputContract childNodeAdded = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode, parentNode);

        // Assert
        childNodeAdded.Should().NotBeNull();
        childNodeAdded.Should().BeOfType<SourceFormatNodeInputContract>();
        childNodeAdded.Id.Should().Be(childNode.Id);
        childNodeAdded.Name!.Should().Be(childNode.Name);

        SourceFormatNodeInputContract childNodeDetails = await Sut.SourceFormatNode
            .GetByIdAsync(childNode.Id);
        childNodeDetails.ParentNodeId.Should().Be(parentNode.Id);

        SourceFormatNodeInputContract parentNodeDetails = await Sut.SourceFormatNode
            .GetByIdAsync(parentNode.Id);
        parentNodeDetails.IsRootNode.Should().Be(1);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchChildEntity()
    {
        // Arrange
        SourceFormatNodeInputContract parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("parent"));

        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(new SourceFormatNodeInputContract { Id = 100 }, parentNode);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Throw_InvalidOperationException_When_NoSuchParentEntity()
    {
        // Arrange
        SourceFormatNodeInputContract childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("parent"));

        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(childNode, new SourceFormatNodeInputContract { Id = 100 });
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}