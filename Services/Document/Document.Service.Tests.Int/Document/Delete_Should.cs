namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class Delete_Should : BaseTest
{
    [Fact]
    public async Task DeleteEntity()
    {
        // Arrange
        List<long> recorded = await CreateDocumentDtoTestData(1);

        // Act
        await Sut.DocumentService.DeleteAsync(recorded[0]);
        List<DocumentGraphqlInput> result = await Sut.DocumentService.GetAllAsync();

        // Assert
        result.Count.Should().Be(0);
    }
}