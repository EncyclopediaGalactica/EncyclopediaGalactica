namespace EncyclopediaGalactica.Services.Document.Dtos.Tests.Unit;

using Contracts.Input;
using FluentAssertions;
using Xunit;

[Trait("Category", "DocumentService")]
public class SourceFormatNodeInputContractShould
{
    [Fact]
    public void DoNotChangeValues()
    {
        // Arrange
        long id = 100;
        string name = "name";
        int isRootNode = 1;
        long parentNodeId = 200;
        long rootNodeId = 300;

        // Act
        SourceFormatNodeInputContract inputContract = new SourceFormatNodeInputContract
        {
            Id = id,
            Name = name,
            IsRootNode = isRootNode,
            ParentNodeId = parentNodeId,
            RootNodeId = rootNodeId
        };

        // Assert
        inputContract.Id.Should().Be(id);
        inputContract.Name.Should().Be(name);
        inputContract.IsRootNode.Should().Be(isRootNode);
        inputContract.ParentNodeId.Should().Be(parentNodeId);
        inputContract.RootNodeId.Should().Be(rootNodeId);
    }
}