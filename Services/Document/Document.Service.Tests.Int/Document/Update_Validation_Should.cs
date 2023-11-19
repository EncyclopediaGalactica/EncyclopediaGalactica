namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using Exceptions;
using FluentAssertions;
using Services.Document.Tests.Datasets.DocumentDto;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class Update_Validation_Should : BaseTest
{
    [Theory]
    [ClassData(typeof(UpdateDocumentDto_InputValidation_InvalidDataset))]
    public async Task Throw_WhenInputIsInvalid(DocumentGraphqlInput graphqlInputGraphqlInput)
    {
        // Arrange & Act
        Func<Task> f = async () =>
        {
            await Sut.DocumentService.UpdateAsync(graphqlInputGraphqlInput.Id, graphqlInputGraphqlInput);
        };

        // Assert
        await f.Should().ThrowExactlyAsync<InvalidInputToDocumentServiceException>();
    }
}