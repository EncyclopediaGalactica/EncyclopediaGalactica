namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdWithParentNodeAsync : BaseTest
{
    [Fact]
    public void Throw_WhenNoSuchElement()
    {
        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.GetByIdWithRootNodeAsync(10); };

        // Assert
        action.Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Return_Node_AndRootNodePropertyIsNull_WhenNoRootNode()
    {
        // Arrange
        SourceFormatNode node = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("name"))
            ;

        //Act
        SourceFormatNode result = await Sut.SourceFormatNodes.GetByIdWithRootNodeAsync(node.Id)
            ;

        // Assert
        result.Id.Should().Be(node.Id);
        result.Name.Should().Be("name");
        result.RootNode.Should().BeNull();
        result.NodesInTheTree.Should().BeEmpty();
    }

    [Fact]
    public async Task Return_Node_AndWithRootNode()
    {
        // Arrange
        SourceFormatNode rootNode = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode
            {
                Name = "root"
            })
            ;
        SourceFormatNode childNode = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode
            {
                Name = "childNode"
            })
            ;
        SourceFormatNode s = await Sut.SourceFormatNodes.AddChildNodeAsync(childNode.Id, rootNode.Id, rootNode.Id)
            ;

        // Act
        SourceFormatNode result = await Sut.SourceFormatNodes.GetByIdWithRootNodeAsync(childNode.Id)
            ;

        // Assert
        result.Id.Should().Be(childNode.Id);
        result.Name.Should().Be("childNode");
        result.IsRootNode.Should().Be(0);
        result.RootNode.Should().NotBeNull();
        result.RootNode.Id.Should().Be(rootNode.Id);
    }
}