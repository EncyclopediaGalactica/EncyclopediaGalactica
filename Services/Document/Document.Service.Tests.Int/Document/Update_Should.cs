namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
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
    public async Task Update(DocumentDto inputDto)
    {
        // Arrange
        List<long> recorded = await CreateDocumentDtoTestData(1);

        // Act
        DocumentDto result = await Sut.DocumentService.UpdateAsync(recorded[0], inputDto);

        // Assert
        result.Id.Should().Be(recorded[0]);
        result.Name.Should().Be(inputDto.Name);
        result.Description.Should().Be(inputDto.Description);
    }
}