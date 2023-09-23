namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
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