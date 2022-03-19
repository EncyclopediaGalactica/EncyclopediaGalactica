namespace EncyclopediaGalactica.SourceFormats.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exceptions;
using FluentAssertions;
using Guards;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdWithTreeValidationShould : BaseTest
{
    [Fact]
    public async Task ThrowWhenInputIsInvalid()
    {
        // Act
        Func<Task> action = async () => { await Sut.GetByIdWithFlatTreeAsync(0).ConfigureAwait(false); };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, GuardValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }
}