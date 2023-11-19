namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class DeleteShould : BaseTest
{
    [Fact]
    public async Task DeleteItem()
    {
        // Arrange
        SourceFormatNodeInputContract nodeResult = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("asd"));

        // Act
        SourceFormatNodeInputContract inputContract = new SourceFormatNodeInputContract { Id = nodeResult.Id };
        await Sut.SourceFormatNode.DeleteAsync(inputContract);

        // Assert
        List<SourceFormatNodeInputContract> list =
            await Sut.SourceFormatNode.GetAllAsync();
        list.Count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteItemWithItsChildren()
    {
        // Arrange
        SourceFormatNodeInputContract otherOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("other"));

        SourceFormatNodeInputContract rootNodeInputContract = new SourceFormatNodeInputContract();
        rootNodeInputContract.Name = "root";
        rootNodeInputContract.IsRootNode = 1;
        SourceFormatNodeInputContract rootNodeResult =
            await Sut.SourceFormatNode.AddAsync(rootNodeInputContract);

        // SourceFormatNode rootOfTheTree = new SourceFormatNode();
        // rootOfTheTree.Id = rootNodeDto.Id;
        // rootOfTheTree.Name = rootNodeDto.Name;
        // rootOfTheTree.RootNodeId = rootNodeResult.Id;
        // SourceFormatNode setupRootNodeResult =
        // await _sourceFormatsService.SourceFormatNodes.UpdateAsync(rootOfTheTree).ConfigureAwait(false);

        // first level
        SourceFormatNodeInputContract firstFirst = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("first_first"));
        SourceFormatNodeInputContract firstFirstResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstFirst, rootNodeResult);

        SourceFormatNodeInputContract firstSecond = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("first_second"));
        SourceFormatNodeInputContract firstSecondResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstSecond, rootNodeResult);

        SourceFormatNodeInputContract firstThird = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("first_third"));
        SourceFormatNodeInputContract firstThirdResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstThird, rootNodeResult);

        // second level - one - parent: first_first
        SourceFormatNodeInputContract secondOneOne =
            await Sut.SourceFormatNode.AddAsync(new SourceFormatNodeInputContract("secondOne_one"));
        SourceFormatNodeInputContract secondOneOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneOne, firstFirst);
        SourceFormatNodeInputContract secondOneTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondOne_two"));
        SourceFormatNodeInputContract secondOneTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneTwo, firstFirst);
        SourceFormatNodeInputContract secondOneThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondOne_three"));
        SourceFormatNodeInputContract secondOneThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneThree, firstFirst);

        // second level - two - parent: first_second
        SourceFormatNodeInputContract secondTwoOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondTwo_one"));
        SourceFormatNodeInputContract secondTwoOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoOne, firstSecond);
        SourceFormatNodeInputContract secondTwoTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondTwo_two"));
        SourceFormatNodeInputContract secondTwoTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoTwo, firstSecond);
        SourceFormatNodeInputContract secondTwoThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondTwo_three"));
        SourceFormatNodeInputContract secondTwoThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoThree, firstSecond);

        // second level - three - parent: first_third
        SourceFormatNodeInputContract secondThreeOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondThree_one"));
        SourceFormatNodeInputContract secondThreeOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeOne, firstThird);
        SourceFormatNodeInputContract secondThreeTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondThree_two"));
        SourceFormatNodeInputContract secondThreeTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeTwo, firstThird);
        SourceFormatNodeInputContract secondThreeThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInputContract("secondThree_three"));
        SourceFormatNodeInputContract secondThreeThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeThree, firstThird);

        // Act
        await Sut.SourceFormatNode.DeleteAsync(rootNodeResult);

        // Assert
        List<SourceFormatNodeInputContract> list = await Sut.SourceFormatNode.GetAllAsync();
        list.Count.Should().Be(1);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchEntity()
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode.DeleteAsync(new SourceFormatNodeInputContract { Id = 100 });
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}