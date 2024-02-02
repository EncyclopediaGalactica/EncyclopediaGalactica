namespace EncyclopediaGalactica.Services.Document.Service.Tests.Unit.DocumentService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using Ctx;
using Document;
using Entities;
using FluentAssertions;
using FluentValidation;
using Mappers;
using Mappers.Interfaces;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Repository.Document;
using Repository.Interfaces;
using Utils.GuardsService;
using Utils.GuardsService.Interfaces;
using Xunit;

[ExcludeFromCodeCoverage]
public class DocumentServiceShould
{
    public static IEnumerable<object[]> ThrowArgumentNullExceptionWhenInjectedIsNullData = new List<object[]>
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
            Substitute.For<IValidator<DocumentInput>>()
        },
        new object[]
        {
            new GuardsService(),
            null,
            new DocumentRepository(
                new DbContextOptions<DocumentDbContext>(),
                Substitute.For<IValidator<Document>>()),
            Substitute.For<IValidator<DocumentInput>>()
        },
        new object[]
        {
            new GuardsService(),
            new SourceFormatMappers(
                Substitute.For<ISourceFormatNodeMappers>(),
                Substitute.For<IDocumentMappers>()),
            null,
            Substitute.For<IValidator<DocumentInput>>()
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
    [MemberData(nameof(ThrowArgumentNullExceptionWhenInjectedIsNullData))]
    public void ThrowArgumentNullException_WhenInjected_IsNull(
        IGuardsService guardsService,
        ISourceFormatMappers mappers,
        IDocumentsRepository documentsRepository,
        IValidator<DocumentInput> documentDtoValidator)
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