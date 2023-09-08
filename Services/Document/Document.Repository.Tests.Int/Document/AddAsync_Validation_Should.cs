namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using FluentValidation;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class AddAsync_Validation_Should : BaseTest
{
    [Fact]
    public void Throw_WhenInput_IsNull()
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.Documents.AddAsync(null!); };
    }

    // [Theory]
    // [MemberData(nameof(DocumentDataset.GetAddValidationScenarioDataset), MemberType = typeof(DocumentDataset))]
    public void Throw_ValidationException_WhenInput_IsInvalid(
        long id,
        string name,
        string desc,
        Uri uri)
    {
        // Arrange && Act
        Func<Task> f = async () =>
        {
            await Sut.Documents.AddAsync(new Document
            {
                Id = id,
                Name = name,
                Description = desc,
                Uri = uri
            });
        };

        // Assert
        f.Should().ThrowExactlyAsync<ValidationException>();
    }
}