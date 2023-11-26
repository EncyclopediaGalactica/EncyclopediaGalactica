namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class DeleteShould : BaseTest
{
    [Fact]
    public async Task DeleteItem()
    {
        // Arrange
        SourceFormatNode node = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("asd"));

        // Act
        await Sut.SourceFormatNodes.DeleteAsync(node.Id);

        // Assert
        List<SourceFormatNode> list = await Sut.SourceFormatNodes.GetAllAsync();
        list.Count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteItemWithItsChildren()
    {
        // Arrange
        SourceFormatNode otherOne =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("other"));

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.SourceFormatNodes.AddAsync(rootNode);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult =
            await Sut.SourceFormatNodes.UpdateAsync(setupRootNode);

        // first level
        SourceFormatNode firstFirst = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("first_first"))
            ;
        SourceFormatNode firstFirstResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id);

        SourceFormatNode firstSecond = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("first_second"))
            ;
        SourceFormatNode firstSecondResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id);

        SourceFormatNode firstThird = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("first_third"))
            ;
        SourceFormatNode firstThirdResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondOne_one"));
        SourceFormatNode secondOneOneResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id);
        SourceFormatNode secondOneTwo =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondOne_two"));
        SourceFormatNode secondOneTwoResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id);
        SourceFormatNode secondOneThree =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondOne_three"));
        SourceFormatNode secondOneThreeResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondTwo_one"));
        SourceFormatNode secondTwoOneResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id);
        SourceFormatNode secondTwoTwo =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondTwo_two"));
        SourceFormatNode secondTwoTwoResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondTwoTwo.Id,
            firstSecond.Id,
            rootNodeResult.Id);
        SourceFormatNode secondTwoThree =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondTwo_three"));
        SourceFormatNode secondTwoThreeResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondTwoThree.Id,
            firstSecond.Id,
            rootNodeResult.Id);

        // second level - three - parent: first_third
        SourceFormatNode secondThreeOne =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondThree_one"));
        SourceFormatNode secondThreeOneResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondThreeOne.Id,
            firstThird.Id,
            rootNodeResult.Id);
        SourceFormatNode secondThreeTwo =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondThree_two"));
        SourceFormatNode secondThreeTwoResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondThreeTwo.Id,
            firstThird.Id,
            rootNodeResult.Id);
        SourceFormatNode secondThreeThree =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("secondThree_three"));
        SourceFormatNode secondThreeThreeResult = await Sut.SourceFormatNodes.AddChildNodeAsync(
            secondThreeThree.Id,
            firstThird.Id,
            rootNodeResult.Id);

        // Act
        await Sut.SourceFormatNodes.DeleteAsync(rootNode.Id);

        // Assert
        List<SourceFormatNode> list = await Sut.SourceFormatNodes.GetAllAsync();
        list.Count.Should().Be(1);
    }

    [Fact]
    public async Task ThrowWhenNoSuchEntity()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.DeleteAsync(100); };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<InvalidOperationException>()
            ;
    }
}