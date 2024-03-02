namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using FluentValidation;
using Services.Document.Tests.Datasets.DocumentDto;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddValidationShould : BaseTest
{
    [Fact]
    public void Throw_WhenInput_IsNull_DocumentInput()
    {
        // Arrange && Act
        Func<Task> f = async () => { await AddDocumentScenario.AddAsync(null!); };
    }

    [Theory]
    [ClassData(typeof(AddDocumentDtoInputValidation_InvalidDataset))]
    public void Throw_ValidationException_WhenInput_IsInvalid(
        Contracts.Input.DocumentInput inputInput)
    {
        // Arrange && Act
        Func<Task> f = async () => { await AddDocumentScenario.AddAsync(inputInput); };

        // Assert
        f.Should().ThrowExactlyAsync<ValidationException>();
    }
}