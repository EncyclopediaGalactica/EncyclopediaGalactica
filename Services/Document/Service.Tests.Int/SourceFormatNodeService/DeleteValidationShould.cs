namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Utils.GuardsService.Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class DeleteValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_ArgumentNullException_WhenInputIsNull()
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode.DeleteAsync(null!)
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Throw_GuardsServiceValueShouldNotBeEqualToException_WhenInputDtoIdIsZero()
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode.DeleteAsync(new SourceFormatNodeDto{Id = 0})
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}