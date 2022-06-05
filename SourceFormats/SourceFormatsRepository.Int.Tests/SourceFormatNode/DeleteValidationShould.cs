namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exceptions;
using FluentAssertions;
using Utils.GuardsService;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatNode Repository Collection")]
public class DeleteValidationShould : BaseTest
{
    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public async Task Throw_WhenInputIsInvalid(long id)
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.DeleteAsync(id).ConfigureAwait(false); };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, GuardsServiceException>()
            .WithInnerExceptionExactly<GuardsServiceException, GuardsServiceValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }
}