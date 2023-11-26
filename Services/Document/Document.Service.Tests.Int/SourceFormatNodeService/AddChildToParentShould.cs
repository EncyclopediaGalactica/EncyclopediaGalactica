namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddChildToParentShould : BaseTest
{
    [Fact]
    public async Task AddChildToParent_WhenParentIsAlreadyInTheTree_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeInput rootNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("rootnode"));
        SourceFormatNodeInput parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("parent"));
        SourceFormatNodeInput childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("child"));

        SourceFormatNodeInput addParentToRoot = await Sut.SourceFormatNode
            .AddChildToParentAsync(parentNode, rootNode);

        // Act
        SourceFormatNodeInput addChildToParent = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode, parentNode);

        // Assert
        addChildToParent.Should().BeOfType<SourceFormatNodeInput>();
        addChildToParent.Should().NotBeNull();
        addChildToParent.Id.Should().BeGreaterThan(0);
        addChildToParent.Name.Should().Be("child");

        SourceFormatNodeInput childNodeDetails = await Sut.SourceFormatNode
            .GetByIdAsync(childNode.Id);
        childNodeDetails.ParentNodeId.Should().Be(parentNode.Id);
        childNodeDetails.RootNodeId.Should().Be(rootNode.Id);
        childNodeDetails.IsRootNode.Should().Be(0);
    }

    [Fact]
    public async Task AddChildToParent_WhenParentIdRootNode_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeInput rootNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("rootnode"));
        SourceFormatNodeInput parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("parent"));
        SourceFormatNodeInput childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("child"));
        SourceFormatNodeInput childNode2 = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("child2"));
        SourceFormatNodeInput addParentToRoot = await Sut.SourceFormatNode
            .AddChildToParentAsync(parentNode, rootNode);
        SourceFormatNodeInput addChildToParent = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode, parentNode);

        // Act
        SourceFormatNodeInput addChild2ToRoot = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode2, rootNode);

        // Assert
        addChild2ToRoot.Should().BeOfType<SourceFormatNodeInput>();
        addChild2ToRoot.Should().NotBeNull();

        SourceFormatNodeInput child2 = await Sut.SourceFormatNode
            .GetByIdAsync(childNode2.Id);
        child2.Should().NotBeNull();
        child2.ParentNodeId.Should().Be(rootNode.Id);
    }

    [Fact]
    public async Task AddChildToParent_WhenParentIsNotRootButShouldBe_AndReturnResponseModelWithChildEntity()
    {
        // Arrange
        SourceFormatNodeInput parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("parent"));
        SourceFormatNodeInput childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("child"));

        // Act
        SourceFormatNodeInput childNodeAdded = await Sut.SourceFormatNode
            .AddChildToParentAsync(childNode, parentNode);

        // Assert
        childNodeAdded.Should().NotBeNull();
        childNodeAdded.Should().BeOfType<SourceFormatNodeInput>();
        childNodeAdded.Id.Should().Be(childNode.Id);
        childNodeAdded.Name!.Should().Be(childNode.Name);

        SourceFormatNodeInput childNodeDetails = await Sut.SourceFormatNode
            .GetByIdAsync(childNode.Id);
        childNodeDetails.ParentNodeId.Should().Be(parentNode.Id);

        SourceFormatNodeInput parentNodeDetails = await Sut.SourceFormatNode
            .GetByIdAsync(parentNode.Id);
        parentNodeDetails.IsRootNode.Should().Be(1);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchChildEntity()
    {
        // Arrange
        SourceFormatNodeInput parentNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("parent"));

        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(new SourceFormatNodeInput { Id = 100 }, parentNode);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Throw_InvalidOperationException_When_NoSuchParentEntity()
    {
        // Arrange
        SourceFormatNodeInput childNode = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("parent"));

        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(childNode, new SourceFormatNodeInput { Id = 100 });
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}