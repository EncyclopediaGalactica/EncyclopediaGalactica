namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Utils.GuardsService.Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatNode Repository Collection")]
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