namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
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
        SourceFormatNodeDto nodeResult = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("asd"));

        // Act
        SourceFormatNodeDto dto = new SourceFormatNodeDto { Id = nodeResult.Id };
        await Sut.SourceFormatNode.DeleteAsync(dto);

        // Assert
        List<SourceFormatNodeDto> list =
            await Sut.SourceFormatNode.GetAllAsync();
        list.Count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteItemWithItsChildren()
    {
        // Arrange
        SourceFormatNodeDto otherOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("other"));

        SourceFormatNodeDto rootNodeDto = new SourceFormatNodeDto();
        rootNodeDto.Name = "root";
        rootNodeDto.IsRootNode = 1;
        SourceFormatNodeDto rootNodeResult =
            await Sut.SourceFormatNode.AddAsync(rootNodeDto);

        // SourceFormatNode rootOfTheTree = new SourceFormatNode();
        // rootOfTheTree.Id = rootNodeDto.Id;
        // rootOfTheTree.Name = rootNodeDto.Name;
        // rootOfTheTree.RootNodeId = rootNodeResult.Id;
        // SourceFormatNode setupRootNodeResult =
        // await _sourceFormatsService.SourceFormatNodes.UpdateAsync(rootOfTheTree).ConfigureAwait(false);

        // first level
        SourceFormatNodeDto firstFirst = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_first"));
        SourceFormatNodeDto firstFirstResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstFirst, rootNodeResult);

        SourceFormatNodeDto firstSecond = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_second"));
        SourceFormatNodeDto firstSecondResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstSecond, rootNodeResult);

        SourceFormatNodeDto firstThird = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_third"));
        SourceFormatNodeDto firstThirdResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(firstThird, rootNodeResult);

        // second level - one - parent: first_first
        SourceFormatNodeDto secondOneOne =
            await Sut.SourceFormatNode.AddAsync(new SourceFormatNodeDto("secondOne_one"));
        SourceFormatNodeDto secondOneOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneOne, firstFirst);
        SourceFormatNodeDto secondOneTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondOne_two"));
        SourceFormatNodeDto secondOneTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneTwo, firstFirst);
        SourceFormatNodeDto secondOneThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondOne_three"));
        SourceFormatNodeDto secondOneThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondOneThree, firstFirst);

        // second level - two - parent: first_second
        SourceFormatNodeDto secondTwoOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_one"));
        SourceFormatNodeDto secondTwoOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoOne, firstSecond);
        SourceFormatNodeDto secondTwoTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_two"));
        SourceFormatNodeDto secondTwoTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoTwo, firstSecond);
        SourceFormatNodeDto secondTwoThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_three"));
        SourceFormatNodeDto secondTwoThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondTwoThree, firstSecond);

        // second level - three - parent: first_third
        SourceFormatNodeDto secondThreeOne = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_one"));
        SourceFormatNodeDto secondThreeOneResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeOne, firstThird);
        SourceFormatNodeDto secondThreeTwo = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_two"));
        SourceFormatNodeDto secondThreeTwoResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeTwo, firstThird);
        SourceFormatNodeDto secondThreeThree = await Sut.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_three"));
        SourceFormatNodeDto secondThreeThreeResult = await Sut.SourceFormatNode
            .AddChildToParentAsync(secondThreeThree, firstThird);

        // Act
        await Sut.SourceFormatNode.DeleteAsync(rootNodeResult);

        // Assert
        List<SourceFormatNodeDto> list = await Sut.SourceFormatNode.GetAllAsync();
        list.Count.Should().Be(1);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchEntity()
    {
        // Act
        Func<Task> task = async () => { await Sut.SourceFormatNode.DeleteAsync(new SourceFormatNodeDto { Id = 100 }); };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}