namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
using FluentAssertions;
using Services.Document.Tests.Datasets;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class AddChildToParentValidation : BaseTest
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_NullInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_ArgumentNullException_WhenInputsAreNull(
        SourceFormatNodeDto child,
        SourceFormatNodeDto parent)
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(child, parent)
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_NullInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_ArgumentNullException_WhenInputsAreInvalid(
        SourceFormatNodeDto child,
        SourceFormatNodeDto parent)
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(child, parent)
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_InvalidInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_GuardsServiceValueShouldNotBeEqualToException_WhenInputNodeIdsAreInvalid(
        SourceFormatNodeDto child,
        SourceFormatNodeDto parent)
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(child, parent)
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}