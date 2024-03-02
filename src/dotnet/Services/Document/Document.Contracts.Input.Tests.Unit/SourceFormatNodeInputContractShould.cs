namespace EncyclopediaGalactica.Services.Document.Dtos.Tests.Unit;

using Contracts.Input;
using FluentAssertions;
using Xunit;

public class SourceFormatNodeInputContractShould
{
    [Fact]
    public void DoNotChangeValues()
    {
        // Arrange
        long id = 100;
        string name = "name";
        int isRootNode = 1;
        long parentNodeId = 300;
        long rootNodeId = 400;

        // Act
        SourceFormatNodeInput input = new SourceFormatNodeInput
        {
            Id = id,
            Name = name,
            IsRootNode = isRootNode,
            ParentNodeId = parentNodeId,
            RootNodeId = rootNodeId
        };

        // Assert
        input.Id.Should().Be(id);
        input.Name.Should().Be(name);
        input.IsRootNode.Should().Be(isRootNode);
        input.ParentNodeId.Should().Be(parentNodeId);
        input.RootNodeId.Should().Be(rootNodeId);
    }
}