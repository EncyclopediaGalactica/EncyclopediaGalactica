namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddShould : BaseTest
{
    [Fact]
    public async Task Add_ANewNode()
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode();
        node.Name = "name";

        // Act
        SourceFormatNode res = await Sut.SourceFormatNodes.AddAsync(node);

        // Assert
        res.Name.Should().Be(node.Name);
    }

    [Fact]
    public async Task Throw_WhenInputIsNull()
    {
        // Arrange & Act
        Func<Task> task = async () => { await Sut.SourceFormatNodes.AddAsync(null!); };

        // Assert
        await task.Should()
                .ThrowExactlyAsync<ArgumentNullException>()
            ;
    }
}