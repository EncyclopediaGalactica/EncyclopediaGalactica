namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class GetByIdWithParentNodeAsyncValidation : BaseTest
{
    [Fact]
    public void Throw_WhenInputIsInvalid()
    {
        // Act
        Func<Task> action = async () =>
        {
            await Sut.SourceFormatNodes.GetByIdWithRootNodeAsync(0).ConfigureAwait(false);
        };

        // Assert
        action.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}