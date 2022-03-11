namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Exceptions;
using FluentAssertions;
using Xunit;

public class AddChildNodeShould : BaseTest
{
    [Fact]
    public async Task AddChildNode()
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
        res.ChildrenSourceFormatNodes.Should().NotBeNull();
        res.ChildrenSourceFormatNodes.Should().NotBeEmpty();
        res.ChildrenSourceFormatNodes.Count.Should().Be(1);
        res.ChildrenSourceFormatNodes.Where(w => w.Id == childNode.Id).Should().NotBeNull();
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
        res2.ChildrenSourceFormatNodes.Should().NotBeNull();
        res2.ChildrenSourceFormatNodes.Should().NotBeEmpty();
        res2.ChildrenSourceFormatNodes.Count.Should().Be(2);
        res2.ChildrenSourceFormatNodes.Where(w => w.Id == childNode1.Id).Should().NotBeNull();
        res2.ChildrenSourceFormatNodes.Where(w => w.Id == childNode2.Id).Should().NotBeNull();
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
        await action.Should().ThrowExactlyAsync<SourceFormatNodeRepositoryException>().ConfigureAwait(false);
    }
}