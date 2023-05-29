namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Unit.DocumentService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;
using EncyclopediaGalactica.Utils.GuardsService;
using EncyclopediaGalactica.Utils.GuardsService.Interfaces;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DocumentService_Should
{
    public static IEnumerable<object[]> ThrowArgumentNullException_WhenInjected_IsNull_Data = new List<object[]>
    {
        new object[]
        {
            null,
            new SourceFormatMappers(
                new Mock<ISourceFormatNodeMappers>().Object,
                new Mock<IDocumentMappers>().Object),
            new DocumentRepository(
                new DbContextOptions<SourceFormatsDbContext>(),
                new Mock<IValidator<Document>>().Object)
        },
        new object[]
        {
            new GuardsService(),
            null,
            new DocumentRepository(
                new DbContextOptions<SourceFormatsDbContext>(),
                new Mock<IValidator<Document>>().Object)
        },
        new object[]
        {
            new GuardsService(),
            new SourceFormatMappers(
                new Mock<ISourceFormatNodeMappers>().Object,
                new Mock<IDocumentMappers>().Object),
            null
        }
    };

    [Theory]
    [MemberData(nameof(ThrowArgumentNullException_WhenInjected_IsNull_Data))]
    public void ThrowArgumentNullException_WhenInjected_IsNull(
        IGuardsService guardsService,
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository)
    {
        // Arrange && Act
        Action action = () =>
        {
            new DocumentService(
                guardsService,
                mappers,
                documentsRepository);
        };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}