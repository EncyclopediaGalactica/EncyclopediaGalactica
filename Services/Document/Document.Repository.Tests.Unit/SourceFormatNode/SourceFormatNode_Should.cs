namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Unit.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Services.Document.SourceFormatsRepository.SourceFormatNode;
using Utils.GuardsService;
using ValidatorService;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class SourceFormatNode_Should
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
        DbContextOptions<DocumentDbContext> options = new DbContextOptionsBuilder<DocumentDbContext>()
            .Options;
        Action action = () => { new SourceFormatNodeRepository(options, null!, new GuardsService()); };

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Throw_WhenInjectedGuardServiceIsNull()
    {
        DbContextOptions<DocumentDbContext> options = new DbContextOptionsBuilder<DocumentDbContext>()
            .Options;
        Action action = () => { new SourceFormatNodeRepository(options, new SourceFormatNodeValidator(), null!); };

        action.Should().ThrowExactly<ArgumentNullException>();
    }
}