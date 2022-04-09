namespace EncyclopediaGalactica.SourceFormats.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exceptions;
using FluentAssertions;
using Utils.GuardsService;
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
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, GuardsServiceException>()
            .WithInnerExceptionExactly<GuardsServiceException, GuardsServiceValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }
}