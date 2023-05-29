namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Utils.GuardsService.Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatNode Repository Collection")]
public class AddChildNodeValidationShould : BaseTest
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public async Task ThrowWhenInputIsInvalid(long childId, long parentId)
    {
        // act
        Func<Task> action = async () =>
        {
            await Sut.SourceFormatNodes.AddChildNodeAsync(childId, parentId, parentId).ConfigureAwait(false);
        };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }
}