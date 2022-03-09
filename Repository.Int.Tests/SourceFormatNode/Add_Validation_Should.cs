namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System;
using System.Threading.Tasks;
using Entities;
using Exceptions;
using FluentAssertions;
using Xunit;

public class Add_Validation_Should : BaseTest
{
    [Theory]
    [InlineData(1, "asd")]
    [InlineData(0, "as")]
    [InlineData(0, "")]
    [InlineData(0, null)]
    [InlineData(0, " ")]
    [InlineData(0, "  ")]
    [InlineData(0, "   ")]
    public void Throw_WhenInputIsInvalid(long id, String name)
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode();
        node.Id = id;
        node.Name = name;

        // Act
        Func<Task> action = async () => { await Sut.AddAsync(node).ConfigureAwait(false); };

        // Assert
        action.Should().ThrowAsync<SourceFormatNodeRepositoryException>();
    }
}