namespace EncyclopediaGalactica.Services.Document.Mappers.Tests.Unit.DocumentMapper;

using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using Document;
using Entities;
using FluentAssertions;
using Interfaces;
using Xunit;

[ExcludeFromCodeCoverage]
public class DocumentInputToDocumentResultMappersShould
{
    private IDocumentMappers _sut = new DocumentMappers();

    [Fact]
    public void MapWithoutChangingValues()
    {
        // Arrange
        long id = 100;
        string name = "name";
        string desc = "desc";
        Uri uri = new Uri("https://bla.com");

        DocumentInput document = new DocumentInput
        {
            Id = id,
            Name = name,
            Description = desc,
            Uri = uri
        };

        // Act
        Document result = _sut.MapDocumentInputToDocument(document);

        // Assert
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);
        result.Description.Should().Be(desc);
        result.Uri.Should().Be(uri);
    }

    [Fact]
    public void MapWhenUriIsNull()
    {
    }
}