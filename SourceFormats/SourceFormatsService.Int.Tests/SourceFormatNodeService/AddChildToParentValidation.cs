namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using QA.Datasets;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class AddChildToParentValidation : BaseTest
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToAsync_NullInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public void Throw_WhenInputIsInvalid(
        SourceFormatNodeDto child,
        SourceFormatNodeDto parent)
    {
        // Act
        Func<Task> action = async () =>
        {
            await _sourceFormatsService.SourceFormatNode.AddChildToParentAsync(
                    child,
                    parent)
                .ConfigureAwait(false);
        };

        // Assert
        action.Should().ThrowExactlyAsync<GuardsServiceValueShouldNoBeNullException>();
    }
}