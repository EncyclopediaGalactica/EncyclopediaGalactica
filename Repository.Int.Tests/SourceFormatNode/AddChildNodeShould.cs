namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Entities;
using Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddChildNodeShould : BaseTest
{
    [Fact]
    public async Task AddChildNode_AndReturnChildWithoutChildren()
    {
        // Arrange
        SourceFormatNode parentNode = await Sut.AddAsync(new SourceFormatNode("asd")).ConfigureAwait(false);
        SourceFormatNode childNode = await Sut.AddAsync(new SourceFormatNode("child")).ConfigureAwait(false);

        // Act
        SourceFormatNode res = await Sut.AddChildNodeAsync(
                childNode.Id,
                parentNode.Id,
                parentNode.Id)
            .ConfigureAwait(false);

        // Assert
        res.ParentNodeId.Should().Be(parentNode.Id);
        res.RootNodeId.Should().Be(parentNode.Id);
        res.ChildrenSourceFormatNodes.Should().NotBeNull();
        res.ChildrenSourceFormatNodes.Should().BeEmpty();
    }

    [Fact]
    public async Task AddManyChildNodes()
    {
        // Arrange
        SourceFormatNode parentNode = await Sut.AddAsync(new SourceFormatNode("asd")).ConfigureAwait(false);
        SourceFormatNode childNode1 = await Sut.AddAsync(new SourceFormatNode("child1")).ConfigureAwait(false);
        SourceFormatNode childNode2 = await Sut.AddAsync(new SourceFormatNode("child2")).ConfigureAwait(false);

        // Act
        SourceFormatNode res1 = await Sut.AddChildNodeAsync(
                childNode1.Id,
                parentNode.Id,
                parentNode.Id)
            .ConfigureAwait(false);
        SourceFormatNode res2 = await Sut.AddChildNodeAsync(
                childNode2.Id,
                parentNode.Id,
                parentNode.Id)
            .ConfigureAwait(false);

        // Assert
        res2.ParentNodeId.Should().Be(parentNode.Id);
        res2.RootNodeId.Should().Be(parentNode.Id);
        res2.ChildrenSourceFormatNodes.Should().NotBeNull();
        res2.ChildrenSourceFormatNodes.Should().BeEmpty();
    }

    [Fact]
    public async Task ThrowWhenChildAlreadyAdded()
    {
        // Arrange
        SourceFormatNode parentNode = await Sut.AddAsync(new SourceFormatNode("parent")).ConfigureAwait(false);
        SourceFormatNode childNode = await Sut.AddAsync(new SourceFormatNode("child")).ConfigureAwait(false);
        SourceFormatNode added = await Sut.AddChildNodeAsync(
                childNode.Id,
                parentNode.Id,
                parentNode.Id)
            .ConfigureAwait(false);

        // Act
        Func<Task> action = async () =>
        {
            await Sut.AddChildNodeAsync(childNode.Id, parentNode.Id, parentNode.Id).ConfigureAwait(false);
        };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, SourceFormatNodeRepositoryException>()
            .ConfigureAwait(false);
    }
}