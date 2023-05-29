namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Utils.GuardsService.Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatNode Repository Collection")]
public class GetByIdWithTreeValidationShould : BaseTest
{
    [Fact]
    public async Task ThrowWhenInputIsInvalid()
    {
        // Act
        Func<Task> action = async () =>
        {
            await Sut.SourceFormatNodes.GetByIdWithFlatTreeAsync(0).ConfigureAwait(false);
        };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }
}