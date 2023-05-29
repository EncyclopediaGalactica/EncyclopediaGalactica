namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Int.Tests.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Utils.GuardsService.Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GetById_Validation_Should : BaseTest
{
    [Fact]
    public void ThrowGuardException_WhenInputIsInvalid()
    {
        // Arrange && Act
        Func<Task> f = async () =>
        {
            await Sut.DocumentService.GetByIdAsync(0).ConfigureAwait(false);
        };

        // Assert
        f.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}