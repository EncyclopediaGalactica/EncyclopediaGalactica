namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class DeleteValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_WhenInputIsInvalid()
    {
        // Act
        Func<Task> f = async () => { await Sut.DocumentService.DeleteAsync(0); };

        // Assert
        await f.Should().ThrowAsync<InvalidInputToDocumentServiceException>();
    }

    [Fact]
    public async Task Throw_NoSuchDocument()
    {
        // Act
        Func<Task> f = async () => { await Sut.DocumentService.DeleteAsync(100); };

        // Assert
        await f.Should().ThrowAsync<NoSuchItemDocumentServiceException>();
    }
}