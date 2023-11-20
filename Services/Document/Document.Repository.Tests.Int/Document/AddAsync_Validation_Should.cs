namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using FluentValidation;
using Services.Document.Tests.Datasets.Document;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddAsyncValidationShould : BaseTest
{
    [Fact]
    public void Throw_WhenInput_IsNull()
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.Documents.AddAsync(null!); };
    }

    [Theory]
    [ClassData(typeof(Add_Validation_InvalidDataset))]
    public void Throw_ValidationException_WhenInput_IsInvalid(Document input)
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.Documents.AddAsync(input); };

        // Assert
        f.Should().ThrowExactlyAsync<ValidationException>();
    }
}