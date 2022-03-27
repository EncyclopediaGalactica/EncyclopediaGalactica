namespace EncyclopediaGalactica.SourceFormats.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exceptions;
using FluentAssertions;
using Guards;
using Xunit;

[ExcludeFromCodeCoverage]
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
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, GuardException>()
            .WithInnerExceptionExactly<GuardException, GuardValueShouldNotBeEqualToException>()
            .ConfigureAwait(false);
    }
}