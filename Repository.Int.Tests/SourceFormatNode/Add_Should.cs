namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using Xunit;

public class Add_Should : BaseTest
{
    [Fact]
    public async Task Add()
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode();
        node.Name = "name";

        // Act
        SourceFormatNode res = await _sut.AddAsync(node);

        // Assert
        res.Name.Should().Be(node.Name);
    }
}