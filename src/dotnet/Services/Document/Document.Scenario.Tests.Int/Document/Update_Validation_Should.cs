namespace EncyclopediaGalactica.Services.Document.Scenario.Tests.Int.Document;

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
public class UpdateValidationShould : BaseTest
{
    [Theory]
    [ClassData(typeof(UpdateDocumentDto_InputValidation_InvalidDataset))]
    public async Task Throw_WhenInputIsInvalid(DocumentInput inputInput)
    {
        // Arrange & Act
        Func<Task> f = async () => { await UpdateDocumentScenario.UpdateAsync(inputInput.Id, inputInput); };

        // Assert
        await f.Should().ThrowExactlyAsync<InvalidInputToDocumentServiceException>();
    }
}