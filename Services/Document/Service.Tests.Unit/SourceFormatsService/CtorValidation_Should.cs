namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Unit.SourceFormatsService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.SourceFormatNodeService;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class CtorValidation_Should
{
    public static IEnumerable<object[]> Throw_WhenAnyCtorParamIsNull_Data => new List<object[]>
    {
        new[]
        {
            null,
            new DocumentService(new GuardsService(),
                new SourceFormatMappers(
                    new Mock<ISourceFormatNodeMappers>().Object,
                    new Mock<IDocumentMappers>().Object),
                new Mock<IDocumentsRepository>().Object)
        },
        new object[] { null!, null! },
        new[]
        {
            new SourceFormatNodeService(
                new SourceFormatNodeDtoValidator(),
                new GuardsService(),
                new SourceFormatMappers(
                    new Mock<ISourceFormatNodeMappers>().Object,
                    new Mock<IDocumentMappers>().Object),
                new SourceFormatNodeRepository(
                    new DbContextOptions<SourceFormatsDbContext>(),
                    new SourceFormatNodeValidator(),
                    new GuardsService()),
                new SourceFormatNodeCacheService(),
                new Mock<ILogger<SourceFormatNodeService>>().Object),
            null
        },
    };

    [Theory]
    [MemberData(nameof(Throw_WhenAnyCtorParamIsNull_Data))]
    public void Throw_WhenAnyCtorParamIsNull(
        ISourceFormatNodeService sourceFormatNodeService,
        IDocumentService documentService)
    {
        // Arrange & Act
        Action action = () =>
        {
            ISourceFormatsService sourceFormatsService = new Services.Document.SourceFormatsService.SourceFormatsService(
                sourceFormatNodeService,
                documentService);
        };

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}