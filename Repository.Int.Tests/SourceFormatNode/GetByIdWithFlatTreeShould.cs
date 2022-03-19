namespace EncyclopediaGalactica.SourceFormats.Repository.Int.Tests.SourceFormatNode;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdWithFlatTreeShould : BaseTest
{
    [Fact]
    public async Task ReturnsFlatTree_One_FirstLevelNode()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
                firstFirst.Id,
                rootNodeResult.Id,
                rootNodeResult.Id)
            .ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(2);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(1);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Two_FirstLevelNodes()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);


        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(2);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Three_FirstLevelNodes()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);


        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(4);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_One_SecondLevelOne()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(5);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(1);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Two_SecondLevelOne()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(6);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(2);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Three_SecondLevelOne()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(7);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneThree.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_One_SecondLevelTwo()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_one")).ConfigureAwait(false);
        SourceFormatNode secondTwoOneResult = await Sut.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(8);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneThree.Id).Should().NotBeNull();
        // second level - two
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes.Count.Should().Be(1);
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoOne.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Two_SecondLevelTwo()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_one")).ConfigureAwait(false);
        SourceFormatNode secondTwoOneResult = await Sut.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoTwo =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_two")).ConfigureAwait(false);
        SourceFormatNode secondTwoTwoResult = await Sut.AddChildNodeAsync(
            secondTwoTwo.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(9);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneThree.Id).Should().NotBeNull();
        // second level - two
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes.Count.Should().Be(2);
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoTwo.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Three_SecondLevelTwo()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_one")).ConfigureAwait(false);
        SourceFormatNode secondTwoOneResult = await Sut.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoTwo =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_two")).ConfigureAwait(false);
        SourceFormatNode secondTwoTwoResult = await Sut.AddChildNodeAsync(
            secondTwoTwo.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoThree =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_three")).ConfigureAwait(false);
        SourceFormatNode secondTwoThreeResult = await Sut.AddChildNodeAsync(
            secondTwoThree.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(10);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneThree.Id).Should().NotBeNull();
        // second level - two
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoThree.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_One_SecondLevelThree()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_one")).ConfigureAwait(false);
        SourceFormatNode secondTwoOneResult = await Sut.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoTwo =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_two")).ConfigureAwait(false);
        SourceFormatNode secondTwoTwoResult = await Sut.AddChildNodeAsync(
            secondTwoTwo.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoThree =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_three")).ConfigureAwait(false);
        SourceFormatNode secondTwoThreeResult = await Sut.AddChildNodeAsync(
            secondTwoThree.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - three - parent: first_third
        SourceFormatNode secondThreeOne =
            await Sut.AddAsync(new SourceFormatNode("secondThree_one")).ConfigureAwait(false);
        SourceFormatNode secondThreeOneResult = await Sut.AddChildNodeAsync(
            secondThreeOne.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(11);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneThree.Id).Should().NotBeNull();
        // second level - two
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoThree.Id).Should().NotBeNull();
        // second level - three
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes.Count.Should().Be(1);
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondThreeOne.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Two_SecondLevelThree()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_one")).ConfigureAwait(false);
        SourceFormatNode secondTwoOneResult = await Sut.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoTwo =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_two")).ConfigureAwait(false);
        SourceFormatNode secondTwoTwoResult = await Sut.AddChildNodeAsync(
            secondTwoTwo.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoThree =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_three")).ConfigureAwait(false);
        SourceFormatNode secondTwoThreeResult = await Sut.AddChildNodeAsync(
            secondTwoThree.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - three - parent: first_third
        SourceFormatNode secondThreeOne =
            await Sut.AddAsync(new SourceFormatNode("secondThree_one")).ConfigureAwait(false);
        SourceFormatNode secondThreeOneResult = await Sut.AddChildNodeAsync(
            secondThreeOne.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondThreeTwo =
            await Sut.AddAsync(new SourceFormatNode("secondThree_two")).ConfigureAwait(false);
        SourceFormatNode secondThreeTwoResult = await Sut.AddChildNodeAsync(
            secondThreeTwo.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(12);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneThree.Id).Should().NotBeNull();
        // second level - two
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoThree.Id).Should().NotBeNull();
        // second level - three
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes.Count.Should().Be(2);
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondThreeOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondThreeTwo.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsFlatTree_Third_SecondLevelThree()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_one")).ConfigureAwait(false);
        SourceFormatNode secondTwoOneResult = await Sut.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoTwo =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_two")).ConfigureAwait(false);
        SourceFormatNode secondTwoTwoResult = await Sut.AddChildNodeAsync(
            secondTwoTwo.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoThree =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_three")).ConfigureAwait(false);
        SourceFormatNode secondTwoThreeResult = await Sut.AddChildNodeAsync(
            secondTwoThree.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - three - parent: first_third
        SourceFormatNode secondThreeOne =
            await Sut.AddAsync(new SourceFormatNode("secondThree_one")).ConfigureAwait(false);
        SourceFormatNode secondThreeOneResult = await Sut.AddChildNodeAsync(
            secondThreeOne.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondThreeTwo =
            await Sut.AddAsync(new SourceFormatNode("secondThree_two")).ConfigureAwait(false);
        SourceFormatNode secondThreeTwoResult = await Sut.AddChildNodeAsync(
            secondThreeTwo.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondThreeThree =
            await Sut.AddAsync(new SourceFormatNode("secondThree_three")).ConfigureAwait(false);
        SourceFormatNode secondThreeThreeResult = await Sut.AddChildNodeAsync(
            secondThreeThree.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(rootNode.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(13);
        // first level
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstFirstResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstSecondResult.Id).Should().NotBeNull();
        res.First(p => p.Id == rootNode.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == firstThirdResult.Id).Should().NotBeNull();
        // second level - one
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstFirst.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondOneThree.Id).Should().NotBeNull();
        // second level - two
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstSecond.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondTwoThree.Id).Should().NotBeNull();
        // second level - three
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondThreeOne.Id).Should().NotBeNull();
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondThreeTwo.Id).Should().NotBeNull();
        res.First(p => p.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(f => f.Id == secondThreeThree.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsPartialFlatTree()
    {
        // Arrange
        SourceFormatNode otherOne = await Sut.AddAsync(new SourceFormatNode("other")).ConfigureAwait(false);

        SourceFormatNode rootNode = new SourceFormatNode();
        rootNode.Name = "root";
        rootNode.IsRootNode = 1;
        SourceFormatNode rootNodeResult = await Sut.AddAsync(rootNode).ConfigureAwait(false);

        SourceFormatNode setupRootNode = new SourceFormatNode();
        setupRootNode.Id = rootNode.Id;
        setupRootNode.Name = rootNode.Name;
        setupRootNode.RootNodeId = rootNodeResult.Id;
        SourceFormatNode setupRootNodeResult = await Sut.UpdateAsync(setupRootNode).ConfigureAwait(false);

        // first level
        SourceFormatNode firstFirst = await Sut.AddAsync(new SourceFormatNode("first_first")).ConfigureAwait(false);
        SourceFormatNode firstFirstResult = await Sut.AddChildNodeAsync(
            firstFirst.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstSecond = await Sut.AddAsync(new SourceFormatNode("first_second")).ConfigureAwait(false);
        SourceFormatNode firstSecondResult = await Sut.AddChildNodeAsync(
            firstSecond.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        SourceFormatNode firstThird = await Sut.AddAsync(new SourceFormatNode("first_third")).ConfigureAwait(false);
        SourceFormatNode firstThirdResult = await Sut.AddChildNodeAsync(
            firstThird.Id,
            rootNodeResult.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNode secondOneOne =
            await Sut.AddAsync(new SourceFormatNode("secondOne_one")).ConfigureAwait(false);
        SourceFormatNode secondOneOneResult = await Sut.AddChildNodeAsync(
            secondOneOne.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneTwo =
            await Sut.AddAsync(new SourceFormatNode("secondOne_two")).ConfigureAwait(false);
        SourceFormatNode secondOneTwoResult = await Sut.AddChildNodeAsync(
            secondOneTwo.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondOneThree =
            await Sut.AddAsync(new SourceFormatNode("secondOne_three")).ConfigureAwait(false);
        SourceFormatNode secondOneThreeResult = await Sut.AddChildNodeAsync(
            secondOneThree.Id,
            firstFirst.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNode secondTwoOne =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_one")).ConfigureAwait(false);
        SourceFormatNode secondTwoOneResult = await Sut.AddChildNodeAsync(
            secondTwoOne.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoTwo =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_two")).ConfigureAwait(false);
        SourceFormatNode secondTwoTwoResult = await Sut.AddChildNodeAsync(
            secondTwoTwo.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondTwoThree =
            await Sut.AddAsync(new SourceFormatNode("secondTwo_three")).ConfigureAwait(false);
        SourceFormatNode secondTwoThreeResult = await Sut.AddChildNodeAsync(
            secondTwoThree.Id,
            firstSecond.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // second level - three - parent: first_third
        SourceFormatNode secondThreeOne =
            await Sut.AddAsync(new SourceFormatNode("secondThree_one")).ConfigureAwait(false);
        SourceFormatNode secondThreeOneResult = await Sut.AddChildNodeAsync(
            secondThreeOne.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondThreeTwo =
            await Sut.AddAsync(new SourceFormatNode("secondThree_two")).ConfigureAwait(false);
        SourceFormatNode secondThreeTwoResult = await Sut.AddChildNodeAsync(
            secondThreeTwo.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);
        SourceFormatNode secondThreeThree =
            await Sut.AddAsync(new SourceFormatNode("secondThree_three")).ConfigureAwait(false);
        SourceFormatNode secondThreeThreeResult = await Sut.AddChildNodeAsync(
            secondThreeThree.Id,
            firstThird.Id,
            rootNodeResult.Id).ConfigureAwait(false);

        // Act
        List<SourceFormatNode> res = await Sut.GetByIdWithFlatTreeAsync(firstThird.Id).ConfigureAwait(false);

        // Assert
        res.Should().NotBeNull();
        res.Count.Should().Be(4);
        res.First(f => f.Id == firstThird.Id).Should().NotBeNull();
        res.First(f => f.Id == firstThird.Id).ChildrenSourceFormatNodes.Count.Should().Be(3);
        res.First(f => f.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(w => w.Id == secondThreeOne.Id).Should().NotBeNull();
        res.First(f => f.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(w => w.Id == secondThreeTwo.Id).Should().NotBeNull();
        res.First(f => f.Id == firstThird.Id).ChildrenSourceFormatNodes
            .First(w => w.Id == secondThreeThree.Id).Should().NotBeNull();
    }
}