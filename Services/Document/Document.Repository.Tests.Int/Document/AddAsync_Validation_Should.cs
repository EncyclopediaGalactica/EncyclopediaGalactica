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
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
public class AddAsync_Validation_Should : BaseTest
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
        Func<Task> f = async () => { await Sut.Documents.AddAsync(input).ConfigureAwait(false); };

        // Assert
        f.Should().ThrowExactlyAsync<ValidationException>();
    }
}