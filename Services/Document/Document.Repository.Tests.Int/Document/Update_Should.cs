namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Document;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using Exceptions;
using FluentAssertions;
using Services.Document.Tests.Datasets.Document;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
public class Update_Should : BaseTest
{
    [Fact]
    public async Task Throw_WhenNoSuchEntity()
    {
        // Act
        Func<Task> f = async () =>
        {
            await Sut.Documents.UpdateAsync(
                100,
                new Document { Id = 100, Name = "asd", Description = "asd" }).ConfigureAwait(false);
        };

        // Assert
        await f.Should().ThrowAsync<DocumentNotFoundException>().ConfigureAwait(false);
    }

    [Theory]
    [ClassData(typeof(Update_ValidDataset))]
    public async Task Update_Entity(Document input)
    {
        // Arrange
        List<long> recorded = await CreateDocumentEntities(1).ConfigureAwait(false);

        // Act
        Document result = await Sut.Documents.UpdateAsync(recorded[0], input).ConfigureAwait(false);

        // Assert
        result.Id.Should().Be(recorded[0]);
        result.Name.Should().Be(input.Name);
        result.Description.Should().Be(input.Description);
    }
}