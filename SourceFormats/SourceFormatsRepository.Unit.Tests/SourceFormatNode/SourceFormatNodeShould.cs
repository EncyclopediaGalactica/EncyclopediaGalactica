namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.Unit.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SourceFormats.SourceFormatsRepository.SourceFormatNode;
using Utils.GuardsService;
using ValidatorService;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeShould
{
    [Fact]
    public void Throw_WhenInjectedServicesAreNull()
    {
        Action action = () => { new SourceFormatNodeRepository(null!, null!, null!); };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedDbOptionsIsNull()
    {
        Action action = () =>
        {
            new SourceFormatNodeRepository(null!, new SourceFormatNodeValidator(), new GuardsService());
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedValidatorIsNull()
    {
        DbContextOptions<SourceFormatsDbContext> options = new DbContextOptionsBuilder<SourceFormatsDbContext>()
            .Options;
        Action action = () => { new SourceFormatNodeRepository(options, null!, new GuardsService()); };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedGuardServiceIsNull()
    {
        DbContextOptions<SourceFormatsDbContext> options = new DbContextOptionsBuilder<SourceFormatsDbContext>()
            .Options;
        Action action = () => { new SourceFormatNodeRepository(options, new SourceFormatNodeValidator(), null!); };

        action.Should().ThrowExactly<ArgumentNullException>();
    }
}