namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Services.Document.Tests.Datasets;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddChildToParentValidation : BaseTest
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_NullInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_ArgumentNullException_WhenInputsAreNull(
        SourceFormatNodeInput child,
        SourceFormatNodeInput parent)
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(child, parent);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_NullInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_ArgumentNullException_WhenInputsAreInvalid(
        SourceFormatNodeInput child,
        SourceFormatNodeInput parent)
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(child, parent);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_InvalidInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_GuardsServiceValueShouldNotBeEqualToException_WhenInputNodeIdsAreInvalid(
        SourceFormatNodeInput child,
        SourceFormatNodeInput parent)
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddChildToParentAsync(child, parent);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}