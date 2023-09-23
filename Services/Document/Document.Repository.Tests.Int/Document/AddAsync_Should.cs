namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
public class AddAsync_Should : BaseTest
{
    [Fact]
    public async Task Throw_DbUpdateException_WhenNameUniqueConstraint_IsViolated()
    {
        // Arrange
        string name = "name";
        Document first = new Document
        {
            Name = name,
            Description = "desc"
        };

        Document firstResult = await Sut.Documents.AddAsync(first).ConfigureAwait(false);

        // Act
        Func<Task> f = async () =>
        {
            await Sut.Documents.AddAsync(new Document { Name = name, Description = "desc" })
                .ConfigureAwait(false);
        };

        // Assert
        await f.Should().ThrowExactlyAsync<DbUpdateException>().ConfigureAwait(false);
    }

    [Fact]
    public async Task Add_Entity_AndReturnTheNewOne()
    {
        // Arrange
        Document first = new Document
        {
            Name = "name",
            Description = "desc"
        };

        // Act
        Document result = await Sut.Documents.AddAsync(first).ConfigureAwait(false);

        // Assert
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(first.Name);
        result.Description.Should().Be(first.Description);
        result.Uri.Should().BeNull();
    }
}