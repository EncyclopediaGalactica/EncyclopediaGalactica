namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class UpdateShould : BaseTest
{
    [Theory]
    [InlineData("new value")]
    public async Task UpdatesValue(string newName)
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode();
        node.Name = "original name";
        SourceFormatNode persistedNode = await Sut.SourceFormatNodes.AddAsync(node);

        SourceFormatNode updatedValues = new SourceFormatNode();
        updatedValues.Name = newName;
        updatedValues.Id = persistedNode.Id;

        // Act
        SourceFormatNode result = await Sut.SourceFormatNodes.UpdateAsync(updatedValues);

        // Assert
        result.Id.Should().Be(persistedNode.Id);
        result.Name.Should().Be(updatedValues.Name);
    }

    [Fact]
    public async Task Throw_WhenNoSuchEntity()
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode();
        node.Id = 100;
        node.Name = "something";

        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.UpdateAsync(node); };

        // Assert
        await action.Should()
                .ThrowExactlyAsync<InvalidOperationException>()
            ;
    }
}