namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Unit.Tests.SourceFormatsService;

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Interfaces;
using Xunit;

[ExcludeFromCodeCoverage]
public class CtorValidationShould
{
    [Theory]
    [InlineData(null)]
    public void Throw_WhenAnyCtorParamIsNull(
        ISourceFormatNodeService sourceFormatNodeService)
    {
        // Arrange & Act
        Action action = () =>
        {
            ISourceFormatsService sourceFormatsService =
                new SourceFormats.SourceFormatsService.SourceFormatsService(sourceFormatNodeService);
        };

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}