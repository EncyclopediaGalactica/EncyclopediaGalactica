namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Entities;
using Exceptions;
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
        SourceFormatNode persistedNode = await Sut.AddAsync(node).ConfigureAwait(false);

        SourceFormatNode updatedValues = new SourceFormatNode();
        updatedValues.Name = newName;
        updatedValues.Id = persistedNode.Id;

        // Act
        SourceFormatNode result = await Sut.UpdateAsync(updatedValues).ConfigureAwait(false);

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
        Func<Task> action = async () => { await Sut.UpdateAsync(node).ConfigureAwait(false); };

        // Assert
        await action.Should().ThrowExactlyAsync<SourceFormatNodeRepositoryException>().ConfigureAwait(false);
    }
}