namespace Repository.Unit.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;
using FluentAssertions;
using Guards;
using Microsoft.EntityFrameworkCore;
using ValidatorService;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeShould
{
    [Fact]
    public void Throw_WhenInjectedServicesAreNull()
    {
        Action action = () => { new SourceFormatNodeRepository(null, null); };

        action.Should().ThrowExactly<GuardValueShouldNoBeNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedContextIsNull()
    {
        Action action = () => { new SourceFormatNodeRepository(null, new SourceFormatNodeValidator()); };

        action.Should().ThrowExactly<GuardValueShouldNoBeNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedValidatorIsNull()
    {
        DbContextOptions<SourceFormatNodeDbContext> options = new DbContextOptionsBuilder<SourceFormatNodeDbContext>()
            .Options;
        SourceFormatNodeDbContext ctx = new SourceFormatNodeDbContext(options);
        Action action = () => { new SourceFormatNodeRepository(ctx, null); };

        action.Should().ThrowExactly<GuardValueShouldNoBeNullException>();
    }
}