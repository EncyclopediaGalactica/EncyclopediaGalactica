namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdShould : BaseTest
{
    [Fact]
    public async Task Return_WithTheDesignatedEntity()
    {
        // Arrange
        string name = "name";
        string desc = "desc";
        Document data = await Sut.Documents.AddAsync(new Document
        {
            Name = name,
            Description = desc
        });

        // Act
        Document result = await Sut.Documents.GetByIdAsync(data.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(name);
        result.Description.Should().Be(desc);
    }

    [Fact]
    public void Throw_InvalidOperationException_WhenNoSuchEntity()
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.Documents.GetByIdAsync(100); };

        // Assert
        f.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}