namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Document;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
public class DeleteAsync_Should : BaseTest
{
    [Fact]
    public async Task Throw_WhenDocumentNotFound()
    {
        // Act
        Func<Task> f = async () => { await Sut.Documents.DeleteAsync(100).ConfigureAwait(false); };

        // Assert
        await f.Should().ThrowAsync<DocumentNotFoundException>().ConfigureAwait(false);
    }

    [Fact]
    public async Task DeleteEntity()
    {
        // Arrange
        List<long> recorded = await CreateDocumentEntities(1);

        // Act
        await Sut.Documents.DeleteAsync(recorded[0]).ConfigureAwait(false);

        // Assert
        List<Document> documents = await Sut.Documents.GetAllAsync().ConfigureAwait(false);
        documents.Count.Should().Be(0);
    }
}