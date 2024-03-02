namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using Contracts.Output;
using Exceptions;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddShould : BaseTest
{
    [Fact]
    public async Task Throw_DbUpdateException_WhenNameUniqueConstraint_IsViolated()
    {
        // Arrange
        string name = "name";
        DocumentInput first = new DocumentInput
        {
            Name = name,
            Description = "desc"
        };

        DocumentResult firstResult = await addDocumentScenario.AddAsync(first);

        // Act
        Func<Task> f = async () =>
        {
            await addDocumentScenario.AddAsync(new DocumentInput { Name = name, Description = "desc" });
        };

        // Assert
        await f.Should().ThrowExactlyAsync<InvalidInputToDocumentServiceException>();
    }

    [Fact]
    public async Task Add_Entity_AndReturnTheNewOne()
    {
        // Arrange
        DocumentInput first = new DocumentInput
        {
            Name = "name",
            Description = "desc"
        };

        // Act
        DocumentResult result = await addDocumentScenario.AddAsync(first);

        // Assert
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(first.Name);
        result.Description.Should().Be(first.Description);
        result.Uri.Should().BeNull();
    }
}