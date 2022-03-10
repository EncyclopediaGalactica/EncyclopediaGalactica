namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Entities;
using Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
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
            SourceFormatNode res = await Sut.AddChildNodeAsync(childId, parentId).ConfigureAwait(false);
        };

        // Assert
        await action.Should().ThrowExactlyAsync<SourceFormatNodeRepositoryException>().ConfigureAwait(false);
    }
}