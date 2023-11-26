namespace EncyclopediaGalactica.Services.Document.Mappers.Tests.Unit;

using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using Document;
using Entities;
using FluentAssertions;
using SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SourceFormatNodeMappers_Should
{
    private readonly SourceFormatMappers _mappers;

    public SourceFormatNodeMappers_Should()
    {
        _mappers = new SourceFormatMappers(
            new SourceFormatNodeMappers(), new DocumentMappers());
    }

    [Fact]
    public void SingleEntityToDto_InFlatFashion()
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode
        {
            Id = 100,
            Name = "name",
            IsRootNode = 1,
        };

        // Act
        SourceFormatNodeInput input = _mappers.SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(node);

        // Assert
        input.Id.Should().Be(node.Id);
        input.Name.Should().Be(node.Name);
        input.IsRootNode.Should().Be(node.IsRootNode);
    }

    [Fact]
    public void SingleDtoToEntity()
    {
        // Arrange
        SourceFormatNodeInput input = new SourceFormatNodeInput
        {
            Id = 100,
            Name = "name",
            IsRootNode = 1
        };

        // Act
        SourceFormatNode node =
            _mappers.SourceFormatNodeMappers.MapSourceFormatNodeDtoToSourceFormatNode(input);

        // Assert
        node.Id.Should().Be(input.Id);
        node.Name.Should().Be(input.Name);
        node.IsRootNode.Should().Be(input.IsRootNode);
    }
}