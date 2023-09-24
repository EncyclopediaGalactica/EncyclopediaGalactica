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
public class DeleteValidationShould : BaseTest
{
    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public async Task Throw_WhenInputIsInvalid(long id)
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.DeleteAsync(id); };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>()
            ;
    }
}