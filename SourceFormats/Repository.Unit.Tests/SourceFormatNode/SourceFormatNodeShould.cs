namespace Repository.Unit.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.SourceFormats.Ctx;
using EncyclopediaGalactica.SourceFormats.Repository.SourceFormatNode;
using EncyclopediaGalactica.SourceFormats.ValidatorService;
using FluentAssertions;
using Guards;
using Microsoft.EntityFrameworkCore;
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
    public void Throw_WhenInjectedContextIsNull()
    {
        Action action = () =>
        {
            new SourceFormatNodeRepository(null!, new SourceFormatNodeValidator(), new GuardService());
        };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedValidatorIsNull()
    {
        DbContextOptions<SourceFormatNodeDbContext> options = new DbContextOptionsBuilder<SourceFormatNodeDbContext>()
            .Options;
        SourceFormatNodeDbContext ctx = new SourceFormatNodeDbContext(options);
        Action action = () => { new SourceFormatNodeRepository(ctx, null!, new GuardService()); };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedGuardServiceIsNull()
    {
        DbContextOptions<SourceFormatNodeDbContext> options = new DbContextOptionsBuilder<SourceFormatNodeDbContext>()
            .Options;
        SourceFormatNodeDbContext ctx = new SourceFormatNodeDbContext(options);
        Action action = () => { new SourceFormatNodeRepository(ctx, new SourceFormatNodeValidator(), null!); };

        action.Should().ThrowExactly<ArgumentNullException>();
    }
}