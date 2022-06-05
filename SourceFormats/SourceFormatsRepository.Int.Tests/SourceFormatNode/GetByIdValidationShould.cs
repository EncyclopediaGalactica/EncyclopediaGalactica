namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.Exceptions;
using EncyclopediaGalactica.Utils.GuardsService;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatNode Repository Collection")]
public class GetByIdValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_WhenIdIsZero()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.GetByIdAsync(0).ConfigureAwait(false); };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, GuardsServiceException>()
            .WithInnerExceptionExactly<GuardsServiceException, GuardsServiceValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }

    [Fact]
    public async Task Throw_WhenNosuchEntity()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.GetByIdAsync(100).ConfigureAwait(false); };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, SourceFormatNodeRepositoryException>()
            .ConfigureAwait(false);
    }
}