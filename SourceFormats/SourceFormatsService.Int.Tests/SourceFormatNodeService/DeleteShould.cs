namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
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
        SourceFormatNodeSingleResultResponseModel nodeResult = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("asd")).ConfigureAwait(false);

        // Act
        SourceFormatNodeDto dto = new SourceFormatNodeDto { Id = nodeResult.Result.Id };
        await _sourceFormatsService.SourceFormatNode.DeleteAsync(dto).ConfigureAwait(false);

        // Assert
        SourceFormatNodeListResultResponseModel list =
            await _sourceFormatsService.SourceFormatNode.GetAllAsync().ConfigureAwait(false);
        list.Result.Count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteItemWithItsChildren()
    {
        // Arrange
        SourceFormatNodeSingleResultResponseModel otherOne = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("other"))
            .ConfigureAwait(false);

        SourceFormatNodeDto rootNodeDto = new SourceFormatNodeDto();
        rootNodeDto.Name = "root";
        rootNodeDto.IsRootNode = 1;
        SourceFormatNodeSingleResultResponseModel rootNodeResult =
            await _sourceFormatsService.SourceFormatNode.AddAsync(rootNodeDto).ConfigureAwait(false);

        // SourceFormatNode rootOfTheTree = new SourceFormatNode();
        // rootOfTheTree.Id = rootNodeDto.Id;
        // rootOfTheTree.Name = rootNodeDto.Name;
        // rootOfTheTree.RootNodeId = rootNodeResult.Id;
        // SourceFormatNode setupRootNodeResult =
        // await _sourceFormatsService.SourceFormatNodes.UpdateAsync(rootOfTheTree).ConfigureAwait(false);

        // first level
        SourceFormatNodeSingleResultResponseModel firstFirst = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_first"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel firstFirstResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(firstFirst.Result!, rootNodeResult.Result!)
            .ConfigureAwait(false);

        SourceFormatNodeSingleResultResponseModel firstSecond = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_second"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel firstSecondResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(firstSecond.Result!, rootNodeResult.Result!)
            .ConfigureAwait(false);

        SourceFormatNodeSingleResultResponseModel firstThird = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("first_third"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel firstThirdResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(firstThird.Result!, rootNodeResult.Result!)
            .ConfigureAwait(false);

        // second level - one - parent: first_first
        SourceFormatNodeSingleResultResponseModel secondOneOne =
            await _sourceFormatsService.SourceFormatNode.AddAsync(new SourceFormatNodeDto("secondOne_one"))
                .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondOneOneResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondOneOne.Result!, firstFirst.Result!)
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondOneTwo = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondOne_two"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondOneTwoResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondOneTwo.Result!, firstFirst.Result!)
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondOneThree = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondOne_three"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondOneThreeResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondOneThree.Result!, firstFirst.Result!)
            .ConfigureAwait(false);

        // second level - two - parent: first_second
        SourceFormatNodeSingleResultResponseModel secondTwoOne = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_one"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondTwoOneResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondTwoOne.Result!, firstSecond.Result!)
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondTwoTwo = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_two"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondTwoTwoResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondTwoTwo.Result!, firstSecond.Result!)
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondTwoThree = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondTwo_three"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondTwoThreeResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondTwoThree.Result!, firstSecond.Result!)
            .ConfigureAwait(false);

        // second level - three - parent: first_third
        SourceFormatNodeSingleResultResponseModel secondThreeOne = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_one"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondThreeOneResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondThreeOne.Result!, firstThird.Result!)
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondThreeTwo = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_two"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondThreeTwoResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondThreeTwo.Result!, firstThird.Result!)
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondThreeThree = await _sourceFormatsService.SourceFormatNode
            .AddAsync(new SourceFormatNodeDto("secondThree_three"))
            .ConfigureAwait(false);
        SourceFormatNodeSingleResultResponseModel secondThreeThreeResult = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(secondThreeThree.Result!, firstThird.Result!)
            .ConfigureAwait(false);

        // Act
        await _sourceFormatsService.SourceFormatNode.DeleteAsync(rootNodeResult.Result!)
            .ConfigureAwait(false);

        // Assert
        SourceFormatNodeListResultResponseModel list = await _sourceFormatsService.SourceFormatNode.GetAllAsync()
            .ConfigureAwait(false);
        list.Result?.Count.Should().Be(1);
    }

    [Fact]
    public async Task ThrowWhenNoSuchEntity()
    {
        // Act
        Func<Task> action = async () =>
        {
            await _sourceFormatsService.SourceFormatNode.DeleteAsync(new SourceFormatNodeDto
            {
                Id = 100
            }).ConfigureAwait(false);
        };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .ConfigureAwait(false);
    }
}