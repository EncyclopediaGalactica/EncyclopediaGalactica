namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using Interfaces;
using Interfaces.SourceFormatNode;
using QA.Datasets;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class AddChildToParentValidation : BaseTest
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_NullInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_WhenInputsAreNull(
        SourceFormatNodeDto child,
        SourceFormatNodeDto parent)
    {
        // Act
        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(child, parent)
            .ConfigureAwait(false);

        // Assert
        result.Should().BeOfType<SourceFormatNodeSingleResultResponseModel>();
        result.Result.Should().BeNull();
        result.Status.Should().Be(SourceFormatsServiceResultStatuses.ValidationError);
        result.IsOperationSuccessful.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.Service_AddChildToParentAsync_NullInput_Dataset),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_WhenInputsAreInvalid(
        SourceFormatNodeDto child,
        SourceFormatNodeDto parent)
    {
        // Act
        SourceFormatNodeSingleResultResponseModel response = await _sourceFormatsService.SourceFormatNode
            .AddChildToParentAsync(child, parent)
            .ConfigureAwait(false);

        // Assert
        response.Should().BeOfType<SourceFormatNodeSingleResultResponseModel>();
        response.Result.Should().BeNull();
        response.Status.Should().Be(SourceFormatsServiceResultStatuses.ValidationError);
        response.IsOperationSuccessful.Should().BeFalse();
    }
}