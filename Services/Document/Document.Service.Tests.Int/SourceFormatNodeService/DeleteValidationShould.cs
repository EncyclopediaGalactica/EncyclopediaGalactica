namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
using FluentAssertions;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class DeleteValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_ArgumentNullException_WhenInputIsNull()
    {
        // Act
        Func<Task> task = async () => { await Sut.SourceFormatNode.DeleteAsync(null!); };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Throw_GuardsServiceValueShouldNotBeEqualToException_WhenInputDtoIdIsZero()
    {
        // Act
        Func<Task> task = async () => { await Sut.SourceFormatNode.DeleteAsync(new SourceFormatNodeDto { Id = 0 }); };

        // Assert
        await task.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}