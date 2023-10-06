namespace EncyclopediaGalactica.Services.Document.Service.Tests.Unit.DocumentService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Document;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Repository.Document;
using EncyclopediaGalactica.Services.Document.Repository.Interfaces;
using EncyclopediaGalactica.Utils.GuardsService;
using EncyclopediaGalactica.Utils.GuardsService.Interfaces;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class DocumentService_Should
{
    public static IEnumerable<object[]> ThrowArgumentNullException_WhenInjected_IsNull_Data = new List<object[]>
    {
        new object[]
        {
            null,
            new SourceFormatMappers(
                Substitute.For<ISourceFormatNodeMappers>(),
                Substitute.For<IDocumentMappers>()),
            new DocumentRepository(
                new DbContextOptions<DocumentDbContext>(),
                Substitute.For<IValidator<Document>>()),
            Substitute.For<IValidator<DocumentDto>>()
        },
        new object[]
        {
            new GuardsService(),
            null,
            new DocumentRepository(
                new DbContextOptions<DocumentDbContext>(),
                Substitute.For<IValidator<Document>>()),
            Substitute.For<IValidator<DocumentDto>>()
        },
        new object[]
        {
            new GuardsService(),
            new SourceFormatMappers(
                Substitute.For<ISourceFormatNodeMappers>(),
                Substitute.For<IDocumentMappers>()),
            null,
            Substitute.For<IValidator<DocumentDto>>()
        },
        new object[]
        {
            new GuardsService(),
            new SourceFormatMappers(
                Substitute.For<ISourceFormatNodeMappers>(),
                Substitute.For<IDocumentMappers>()),
            new DocumentRepository(
                new DbContextOptions<DocumentDbContext>(),
                Substitute.For<IValidator<Document>>()),
            null
        }
    };

    [Theory]
    [MemberData(nameof(ThrowArgumentNullException_WhenInjected_IsNull_Data))]
    public void ThrowArgumentNullException_WhenInjected_IsNull(
        IGuardsService guardsService,
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository,
        IValidator<DocumentDto> documentDtoValidator)
    {
        // Arrange && Act
        Action action = () =>
        {
            new DocumentService(
                guardsService,
                mappers,
                documentsRepository,
                documentDtoValidator);
        };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}