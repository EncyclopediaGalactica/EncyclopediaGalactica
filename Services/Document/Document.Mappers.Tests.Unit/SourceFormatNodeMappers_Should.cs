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
[Trait("Category", "DocumentService")]
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
        SourceFormatNodeInputContract inputContract = _mappers.SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(node);

        // Assert
        inputContract.Id.Should().Be(node.Id);
        inputContract.Name.Should().Be(node.Name);
        inputContract.IsRootNode.Should().Be(node.IsRootNode);
    }

    [Fact]
    public void SingleDtoToEntity()
    {
        // Arrange
        SourceFormatNodeInputContract inputContract = new SourceFormatNodeInputContract
        {
            Id = 100,
            Name = "name",
            IsRootNode = 1
        };

        // Act
        SourceFormatNode node =
            _mappers.SourceFormatNodeMappers.MapSourceFormatNodeDtoToSourceFormatNode(inputContract);

        // Assert
        node.Id.Should().Be(inputContract.Id);
        node.Name.Should().Be(inputContract.Name);
        node.IsRootNode.Should().Be(inputContract.IsRootNode);
    }
}