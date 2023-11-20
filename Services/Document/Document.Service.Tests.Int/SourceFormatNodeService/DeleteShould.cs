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
public class DeleteShould : BaseTest
{
    [Fact]
    public async Task DeleteItem()
    {
        // Arrange
        SourceFormatNodeInput nodeResult = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("asd"));

        // Act
        SourceFormatNodeInput input = new SourceFormatNodeInput { Id = nodeResult.Id };
        await Sut.SourceFormatNode.DeleteAsync(input);

        // Assert
        List<SourceFormatNodeInput> list =
            await Sut.SourceFormatNode.GetAllAsync();
        list.Count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteItemWithItsChildren()
    {
        // Arrange
        SourceFormatNodeInput otherOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("other"));

        SourceFormatNodeInput rootNodeInput = new SourceFormatNodeInput();
        rootNodeInput.Name = "root";
        rootNodeInput.IsRootNode = 1;
        SourceFormatNodeInput rootNodeResult =
            await Sut.SourceFormatNode.AddAsync(rootNodeInput);

        // SourceFormatNode rootOfTheTree = new SourceFormatNode();
        // rootOfTheTree.Id = rootNodeDto.Id;
        // rootOfTheTree.Name = rootNodeDto.Name;
        // rootOfTheTree.RootNodeId = rootNodeResult.Id;
        // SourceFormatNode setupRootNodeResult =
        // await _sourceFormatsService.SourceFormatNodes.UpdateAsync(rootOfTheTree).ConfigureAwait(false);

        // first level
        SourceFormatNodeInput firstFirst = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("first_first"));
        SourceFormatNodeInput firstFirstResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstFirst, rootNodeResult);

        SourceFormatNodeInput firstSecond = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("first_second"));
        SourceFormatNodeInput firstSecondResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstSecond, rootNodeResult);

        SourceFormatNodeInput firstThird = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("first_third"));
        SourceFormatNodeInput firstThirdResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstThird, rootNodeResult);

        // second level - one - parent: first_first
        SourceFormatNodeInput secondOneOne =
            await Sut.SourceFormatNode.AddAsync(new SourceFormatNodeInput("secondOne_one"));
        SourceFormatNodeInput secondOneOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneOne, firstFirst);
        SourceFormatNodeInput secondOneTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondOne_two"));
        SourceFormatNodeInput secondOneTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneTwo, firstFirst);
        SourceFormatNodeInput secondOneThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondOne_three"));
        SourceFormatNodeInput secondOneThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneThree, firstFirst);

        // second level - two - parent: first_second
        SourceFormatNodeInput secondTwoOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondTwo_one"));
        SourceFormatNodeInput secondTwoOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoOne, firstSecond);
        SourceFormatNodeInput secondTwoTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondTwo_two"));
        SourceFormatNodeInput secondTwoTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoTwo, firstSecond);
        SourceFormatNodeInput secondTwoThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondTwo_three"));
        SourceFormatNodeInput secondTwoThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoThree, firstSecond);

        // second level - three - parent: first_third
        SourceFormatNodeInput secondThreeOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondThree_one"));
        SourceFormatNodeInput secondThreeOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeOne, firstThird);
        SourceFormatNodeInput secondThreeTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondThree_two"));
        SourceFormatNodeInput secondThreeTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeTwo, firstThird);
        SourceFormatNodeInput secondThreeThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeInput("secondThree_three"));
        SourceFormatNodeInput secondThreeThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeThree, firstThird);

        // Act
        await Sut.SourceFormatNode.DeleteAsync(rootNodeResult);

        // Assert
        List<SourceFormatNodeInput> list = await Sut.SourceFormatNode.GetAllAsync();
        list.Count.Should().Be(1);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchEntity()
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode.DeleteAsync(new SourceFormatNodeInput { Id = 100 });
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}