namespace EncyclopediaGalactica.Services.Document.Mappers.Tests.Unit;

using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using Document;
using Entities;
using FluentAssertions;
using Interfaces;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class DocumentMappers_Should
{
    private IDocumentMappers _sut = new DocumentMappers();

    [Fact]
    public void Map_DocumentToDto_WithoutChangingValues()
    {
        // Arrange
        long id = 100;
        string name = "name";
        string desc = "desc";
        Uri uri = new Uri("https://bla.com");

        Document document = new Document
        {
            Id = id,
            Name = name,
            Description = desc,
            Uri = uri
        };

        // Act
        DocumentGraphqlInput result = _sut.MapDocumentToDocumentDto(document);

        // Assert
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);
        result.Description.Should().Be(desc);
        result.Uri.Should().Be(uri);
    }

    [Fact]
    public void Map_DocumentToDto_WithoutChangingValues_WhenUriIsNull()
    {
        // Arrange
        long id = 100;
        string name = "name";
        string desc = "desc";

        Document document = new Document
        {
            Id = id,
            Name = name,
            Description = desc,
        };

        // Act
        DocumentGraphqlInput result = _sut.MapDocumentToDocumentDto(document);

        // Assert
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);
        result.Description.Should().Be(desc);
        result.Uri.Should().BeNull();
    }

    [Fact]
    public void Map_DtoToDocument_WithoutChangingValues()
    {
    }

    [Fact]
    public void Map_DtoToDocument_WithoutChangingValues_WhenUriIsNull()
    {
    }
}