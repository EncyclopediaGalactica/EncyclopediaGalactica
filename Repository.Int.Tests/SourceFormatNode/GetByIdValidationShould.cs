namespace EncyclopediaGalactica.SourceFormats.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exceptions;
using FluentAssertions;
using Guards;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_WhenIdIsZero()
    {
        // Act
        Func<Task> action = async () => { await Sut.GetByIdAsync(0).ConfigureAwait(false); };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, GuardValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }

    [Fact]
    public async Task Throw_WhenNosuchEntity()
    {
        // Act
        Func<Task> action = async () => { await Sut.GetByIdAsync(100).ConfigureAwait(false); };

        // Assert
        await action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, SourceFormatNodeRepositoryException>()
            .ConfigureAwait(false);
    }
}