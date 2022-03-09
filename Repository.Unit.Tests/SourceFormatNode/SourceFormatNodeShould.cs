namespace Repository.Unit.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
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
    public async Task Throw_WhenInjectedServicesAreNull()
    {
        Func<Task> action = async () => { new SourceFormatNodeRepository(null, null); };

        await action.Should().ThrowExactlyAsync<GuardAgainstException>();
    }

    [Fact]
    public async Task Throw_WhenInjectedContextIsNull()
    {
        Func<Task> action = async () => { new SourceFormatNodeRepository(null, new SourceFormatNodeValidator()); };

        await action.Should().ThrowExactlyAsync<GuardAgainstException>();
    }

    [Fact]
    public async Task Throw_WhenInjectedValidatorIsNull()
    {
        DbContextOptions<SourceFormatNodeDbContext> options = new DbContextOptionsBuilder<SourceFormatNodeDbContext>()
            .Options;
        SourceFormatNodeDbContext ctx = new SourceFormatNodeDbContext(options);
        Func<Task> action = async () => { new SourceFormatNodeRepository(ctx, null); };

        await action.Should().ThrowExactlyAsync<GuardAgainstException>();
    }
}