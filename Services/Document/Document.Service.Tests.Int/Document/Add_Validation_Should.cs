namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
using FluentAssertions;
using FluentValidation;
using Services.Document.Tests.Datasets.DocumentDto;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class Add_Validation_Should : BaseTest
{
    [Fact]
    public void Throw_WhenInput_IsNull()
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.DocumentService.AddAsync(null!); };
    }

    [Theory]
    [ClassData(typeof(AddDocumentDtoInputValidation_InvalidDataset))]
    public void Throw_ValidationException_WhenInput_IsInvalid(DocumentDto inputDto)
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.DocumentService.AddAsync(inputDto); };

        // Assert
        f.Should().ThrowExactlyAsync<ValidationException>();
    }
}