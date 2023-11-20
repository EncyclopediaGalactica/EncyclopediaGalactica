namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetAllAsyncShould : BaseTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task ReturnAll(int amount)
    {
        // Arrange
        await CreateDocumentEntities(amount);

        // Act
        List<Document> result = await Sut.Documents.GetAllAsync();

        // Assert
        result.Count.Should().Be(amount);
    }
}