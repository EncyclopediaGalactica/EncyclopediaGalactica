namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Services.Document.Tests.Datasets.DocumentDto;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class Update_Should : BaseTest
{
    [Theory]
    [ClassData(typeof(UpdateDocumentDto_Update_Dataset))]
    public async Task Update(DocumentGraphqlInput graphqlInputGraphqlInput)
    {
        // Arrange
        List<long> recorded = await CreateDocumentDtoTestData(1);

        // Act
        DocumentGraphqlInput result = await Sut.DocumentService.UpdateAsync(recorded[0], graphqlInputGraphqlInput);

        // Assert
        result.Id.Should().Be(recorded[0]);
        result.Name.Should().Be(graphqlInputGraphqlInput.Name);
        result.Description.Should().Be(graphqlInputGraphqlInput.Description);
    }
}