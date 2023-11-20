namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdWithTreeValidationShould : BaseTest
{
    [Fact]
    public async Task ThrowWhenInputIsInvalid()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.GetByIdWithFlatTreeAsync(0); };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>()
            ;
    }
}