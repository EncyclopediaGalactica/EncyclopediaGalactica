namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using FluentAssertions;
using FluentValidation;
using Xunit;

[ExcludeFromCodeCoverage]
public class DeleteAsyncValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_WhenInputIsInvalid()
    {
        // Act
        Func<Task> f = async () => { await Sut.Documents.DeleteAsync(0); };

        // Assert
        await f.Should().ThrowAsync<ValidationException>();
    }
}