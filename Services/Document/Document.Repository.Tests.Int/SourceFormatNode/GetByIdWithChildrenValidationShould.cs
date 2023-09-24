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
public class GetByIdWithChildrenValidationShould : BaseTest
{
    [Fact]
    public async Task ThrowWhenInputIsInvalid()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.GetByIdWithChildrenAsync(0); };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>()
            ;
    }
}