namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Interfaces;
using Interfaces.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class DeleteShould : BaseTest
{
    [Fact]
    public async Task DeleteItem()
    {
        // Arrange
        SourceFormatNodeDto nodeResult = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("asd")).ConfigureAwait(false);

        // Act
        SourceFormatNodeDto dto = new SourceFormatNodeDto { Id = nodeResult.Id };
        await _sourceFormatsService.SourceFormatNode.DeleteAsync(dto).ConfigureAwait(false);

        // Assert
        List<SourceFormatNodeDto> list =
            await _sourceFormatsService.SourceFormatNode.GetAllAsync().ConfigureAwait(false);
        list.Count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteItemWithItsChildren()
    {
        // Arrange
        SourceFormatNodeDto otherOne = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("other"))
            .ConfigureAwait(false);

        SourceFormatNodeDto rootNodeDto = new SourceFormatNodeDto();
        rootNodeDto.Name = "root";
        rootNodeDto.IsRootNode = 1;
        SourceFormatNodeDto rootNodeResult =
            await _sourceFormatsService.SourceFormatNode.AddAsync(rootNodeDto).ConfigureAwait(false);

        // SourceFormatNode rootOfTheTree = new SourceFormatNode();
        // rootOfTheTree.Id = rootNodeDto.Id;
        // rootOfTheTree.Name = rootNodeDto.Name;
        // rootOfTheTree.RootNodeId = rootNodeResult.Id;
        // SourceFormatNode setupRootNodeResult =
        // await _sourceFormatsService.SourceFormatNodes.UpdateAsync(rootOfTheTree).ConfigureAwait(false);

        // first level
        SourceFormatNodeDto firstFirst = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_first"))
            .ConfigureAwait(false);
        SourceFormatNodeDto firstFirstResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(firstFirst, rootNodeResult)
            .ConfigureAwait(false);

        SourceFormatNodeDto firstSecond = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_second"))
            .ConfigureAwait(false);
        SourceFormatNodeDto firstSecondResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(firstSecond, rootNodeResult)
            .ConfigureAwait(false);

        SourceFormatNodeDto firstThird = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_third"))
            .ConfigureAwait(false);
        SourceFormatNodeDto firstThirdResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(firstThird, rootNodeResult)
            .ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNodeDto secondOneOne =
            await _sourceFormatsService.SourceFormatNode.AddAsync(new SourceFormatNodeDto("secondOne_one"))
                .ConfigureAwait(false);
        SourceFormatNodeDto secondOneOneResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondOneOne, firstFirst)
            .ConfigureAwait(false);
        SourceFormatNodeDto secondOneTwo = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondOne_two"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondOneTwoResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondOneTwo, firstFirst)
            .ConfigureAwait(false);
        SourceFormatNodeDto secondOneThree = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondOne_three"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondOneThreeResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondOneThree, firstFirst)
            .ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNodeDto secondTwoOne = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_one"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondTwoOneResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondTwoOne, firstSecond)
            .ConfigureAwait(false);
        SourceFormatNodeDto secondTwoTwo = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_two"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondTwoTwoResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondTwoTwo, firstSecond)
            .ConfigureAwait(false);
        SourceFormatNodeDto secondTwoThree = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_three"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondTwoThreeResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondTwoThree, firstSecond)
            .ConfigureAwait(false);

        // second level - three - parent: first_third
        SourceFormatNodeDto secondThreeOne = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_one"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondThreeOneResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondThreeOne, firstThird)
            .ConfigureAwait(false);
        SourceFormatNodeDto secondThreeTwo = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_two"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondThreeTwoResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondThreeTwo, firstThird)
            .ConfigureAwait(false);
        SourceFormatNodeDto secondThreeThree = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_three"))
            .ConfigureAwait(false);
        SourceFormatNodeDto secondThreeThreeResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondThreeThree, firstThird)
            .ConfigureAwait(false);

        // Act
        await _sourceFormatsService.SourceFormatNode.DeleteAsync(rootNodeResult)
            .ConfigureAwait(false);

        // Assert
        List<SourceFormatNodeDto> list = await _sourceFormatsService.SourceFormatNode.GetAllAsync()
            .ConfigureAwait(false);
        list.Count.Should().Be(1);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchEntity()
    {
        // Act
        Func<Task> task = async () =>
        {
            await _sourceFormatsService.SourceFormatNode.DeleteAsync(new SourceFormatNodeDto { Id = 100 })
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}