namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Unit.SourceFormatsService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ctx;
using Document;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.SourceFormatNode;
using Utils.GuardsService;
using ValidatorService;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatsServiceShould
{
    public static IEnumerable<object[]> ThrowArgumentNullExceptionWhenInjectedIsNullData = new List<object[]>
    {
        new[]
        {
            null, new DocumentRepository(
                new DbContextOptions<DocumentDbContext>(),
                new DocumentValidator())
        },
        new[]
        {
            new SourceFormatNodeRepository(
                new DbContextOptions<DocumentDbContext>(),
                new SourceFormatNodeValidator(),
                new GuardsService()),
            null
        },
        new object[] { null, null }
    };

    [Theory]
    [MemberData(nameof(ThrowArgumentNullExceptionWhenInjectedIsNullData))]
    public void Throw_ArgumentNullException_WhenInjectedIsNull(
        ISourceFormatNodeRepository sourceFormatNodeRepository,
        IDocumentsRepository documentsRepository)
    {
        // Assert
        Action a = () => { new SourceFormatsRepository(sourceFormatNodeRepository, documentsRepository); };
    }
}