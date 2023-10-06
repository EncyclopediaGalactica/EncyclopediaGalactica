namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class GetByIdValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_GuardsServiceValueShouldNotBeEqualToException_WhenIdIsZero()
    {
        // Act
        Func<Task> task = async () => { await Sut.SourceFormatNode.GetByIdAsync(0); };

        // Assert
        await task.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}