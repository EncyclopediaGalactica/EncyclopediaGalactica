namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Int.Tests.SourceFormatNode;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdWithChildrenShould : BaseTest
{
    [Fact]
    public async Task ReturnValue()
    {
        // Arrange
        (int childAmount, long childId, long parentId, long rootNodeId) prep =
            await PrepareSourceFormatNodeRepoWith_OneParentAnd_OneChild();

        // Act
        SourceFormatNode res = await Sut.GetByIdWithChildrenAsync(prep.parentId).ConfigureAwait(false);

        // Assert
        res.ChildrenSourceFormatNodes.Should().NotBeNull();
        res.ChildrenSourceFormatNodes.Should().NotBeEmpty();
        res.ChildrenSourceFormatNodes.Count.Should().Be(prep.childAmount);
    }

    [Fact]
    public async Task ReturnNullWhenNoResult()
    {
        // Arrange
        SourceFormatNode rep = await Sut.AddAsync(new SourceFormatNode()).ConfigureAwait(false);

        // Act
        SourceFormatNode result = await Sut.GetByIdWithChildrenAsync(rep.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.ChildrenSourceFormatNodes.Should().NotBeNull();
        result.ChildrenSourceFormatNodes.Should().BeEmpty();
    }
}