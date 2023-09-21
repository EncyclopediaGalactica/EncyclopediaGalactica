namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int.Document;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
public class GetById_Should : BaseTest
{
    [Fact]
    public async Task Return_WithTheDto()
    {
        // Arrange
        string name = "name";
        string desc = "desc";
        DocumentDto data = await Sut.DocumentService.AddAsync(new DocumentDto
        {
            Name = name,
            Description = desc
        }).ConfigureAwait(false);

        // Act
        DocumentDto result = await Sut.DocumentService.GetByIdAsync(data.Id).ConfigureAwait(false);

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
        Func<Task> f = async () => { await Sut.DocumentService.GetByIdAsync(100).ConfigureAwait(false); };

        // Assert
        f.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}