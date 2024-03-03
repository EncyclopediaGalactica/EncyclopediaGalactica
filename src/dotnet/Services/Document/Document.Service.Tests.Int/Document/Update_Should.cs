namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using Contracts.Output;
using FluentAssertions;
using Services.Document.Tests.Datasets.DocumentDto;
using Xunit;

[ExcludeFromCodeCoverage]
public class UpdateShould : BaseTest
{
    [Theory]
    [ClassData(typeof(UpdateDocumentDto_Update_Dataset))]
    public async Task Update(DocumentInput inputInput)
    {
        // Arrange
        List<long> recorded = await CreateDocumentDtoTestData(1);

        // Act
        DocumentResult result = await UpdateDocumentScenario.UpdateAsync(recorded[0], inputInput);

        // Assert
        result.Id.Should().Be(recorded[0]);
        result.Name.Should().Be(inputInput.Name);
        result.Description.Should().Be(inputInput.Description);
    }
}