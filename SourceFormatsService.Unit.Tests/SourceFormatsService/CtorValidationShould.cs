namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Unit.Tests.SourceFormatsService;

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Guards;
using Interfaces;
using Xunit;

public class CtorValidationShould
{
    [Theory]
    [InlineData(null)]
    public async Task Throw_WhenAnyCtorParamIsNull(
        ISourceFormatNodeService sourceFormatNodeService)
    {
        // Arrange & Act
        Action action = () =>
        {
            ISourceFormatsService sourceFormatsService =
                new SourceFormats.SourceFormatsService.SourceFormatsService(sourceFormatNodeService);
        };

        // Assert
        action.Should().Throw<GuardException>();
    }
}