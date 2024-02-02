namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using Contracts.Output;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdShould : BaseTest
{
    [Fact]
    public async Task Return_WithTheDto()
    {
        // Arrange
        string name = "name";
        string desc = "desc";
        DocumentResult data = await Sut.DocumentService.AddAsync(new DocumentInput
        {
            Name = name,
            Description = desc
        });

        // Act
        DocumentResult result = await Sut.DocumentService.GetByIdAsync(data.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(data.Name);
        result.Description.Should().Be(data.Description);
    }

    [Fact]
    public void Throw_InvalidOperationException_WhenNoSuchElement()
    {
        // Arrange && Act
        Func<Task> f = async () => { await Sut.DocumentService.GetByIdAsync(100); };

        // Assert
        f.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}