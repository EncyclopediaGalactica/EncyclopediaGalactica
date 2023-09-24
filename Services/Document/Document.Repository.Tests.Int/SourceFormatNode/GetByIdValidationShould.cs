namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
public class GetByIdValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_WhenIdIsZero()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.GetByIdAsync(0); };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>()
            ;
    }

    [Fact]
    public async Task Throw_WhenNoSuchEntity()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.GetByIdAsync(100); };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<InvalidOperationException>()
            ;
    }
}