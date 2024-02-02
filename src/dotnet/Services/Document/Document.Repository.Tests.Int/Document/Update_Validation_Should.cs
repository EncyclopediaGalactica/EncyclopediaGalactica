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
public class UpdateValidationShould : BaseTest
{
    [Theory]
    [ClassData(typeof(Update_Validation_InvalidDataset))]
    public async Task Throw_WhenInputIsInvalid(Document input)
    {
        // Act
        Func<Task> f = async () => { await Sut.Documents.UpdateAsync(input.Id, input); };

        // Assert
        await f.Should().ThrowExactlyAsync<ValidationException>();
    }
}