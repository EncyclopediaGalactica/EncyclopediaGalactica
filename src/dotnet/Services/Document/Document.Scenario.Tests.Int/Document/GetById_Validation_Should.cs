namespace EncyclopediaGalactica.Services.Document.Scenario.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdValidationShould : BaseTest
{
    [Fact]
    public void ThrowGuardException_WhenInputIsInvalid()
    {
        // Arrange && Act
        Func<Task> f = async () => { await GetDocumentByIdScenario.GetByIdAsync(0); };

        // Assert
        f.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }
}