namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
public class AddChildNodeShould : BaseTest
{
    [Fact]
    public async Task AddChildNode_AndReturnChildWithoutChildren()
    {
        // Arrange
        SourceFormatNode parentNode = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("asd"))
            ;
        SourceFormatNode childNode = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("child"))
            ;

        // Act
        SourceFormatNode res = await Sut.SourceFormatNodes.AddChildNodeAsync(
                childNode.Id,
                parentNode.Id,
                parentNode.Id)
            ;

        // Assert
        res.ParentNodeId.Should().Be(parentNode.Id);
        res.RootNodeId.Should().Be(parentNode.Id);
        res.ChildrenSourceFormatNodes.Should().NotBeNull();
        res.ChildrenSourceFormatNodes.Should().BeEmpty();
    }

    [Fact]
    public async Task AddManyChildNodes()
    {
        // Arrange
        SourceFormatNode parentNode =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("asd"));
        SourceFormatNode childNode1 =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("child1"));
        SourceFormatNode childNode2 =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("child2"));

        // Act
        SourceFormatNode res1 = await Sut.SourceFormatNodes.AddChildNodeAsync(
                childNode1.Id,
                parentNode.Id,
                parentNode.Id)
            ;
        SourceFormatNode res2 = await Sut.SourceFormatNodes.AddChildNodeAsync(
                childNode2.Id,
                parentNode.Id,
                parentNode.Id)
            ;

        // Assert
        res2.ParentNodeId.Should().Be(parentNode.Id);
        res2.RootNodeId.Should().Be(parentNode.Id);
        res2.ChildrenSourceFormatNodes.Should().NotBeNull();
        res2.ChildrenSourceFormatNodes.Should().BeEmpty();
    }

    [Fact]
    public async Task ThrowWhenChildAlreadyAdded()
    {
        // Arrange
        SourceFormatNode parentNode =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("parent"));
        SourceFormatNode childNode =
            await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("child"));
        SourceFormatNode added = await Sut.SourceFormatNodes.AddChildNodeAsync(
                childNode.Id,
                parentNode.Id,
                parentNode.Id)
            ;

        // Act
        Func<Task> action = async () =>
        {
            await Sut.SourceFormatNodes.AddChildNodeAsync(childNode.Id, parentNode.Id, parentNode.Id)
                ;
        };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<InvalidOperationException>()
            ;
    }
}