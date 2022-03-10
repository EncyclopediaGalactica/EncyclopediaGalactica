namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdWithChildrenValidationShould : BaseTest
{
    [Fact]
    public async Task ThrowWhenInputIsInvalid()
    {
        // Act
        Func<Task> action = async () => { await Sut.GetByIdWithChildrenAsync(0).ConfigureAwait(false); };

        // Assert
        await action.Should().ThrowAsync<SourceFormatNodeRepositoryException>().ConfigureAwait(false);
    }
}