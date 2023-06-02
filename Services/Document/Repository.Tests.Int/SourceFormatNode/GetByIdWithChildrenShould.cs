namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int.SourceFormatNode;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Services.Document.Entities;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class GetByIdWithChildrenShould : BaseTest
{
    [Fact]
    public async Task ReturnValue()
    {
        // Arrange
        (int childAmount, long childId, long parentId, long rootNodeId) prep =
            await PrepareSourceFormatNodeRepoWith_OneParentAnd_OneChild()
                .ConfigureAwait(false);

        // Act
        SourceFormatNode res =
            await Sut.SourceFormatNodes.GetByIdWithChildrenAsync(prep.parentId).ConfigureAwait(false);

        // Assert
        res.ChildrenSourceFormatNodes.Should().NotBeNull();
        res.ChildrenSourceFormatNodes.Should().NotBeEmpty();
        res.ChildrenSourceFormatNodes.Count.Should().Be(prep.childAmount);
    }

    [Fact]
    public async Task ReturnNullWhenNoResult()
    {
        // Arrange
        SourceFormatNode rep = await Sut.SourceFormatNodes.AddAsync(new SourceFormatNode("asd")).ConfigureAwait(false);

        // Act
        SourceFormatNode result = await Sut.SourceFormatNodes.GetByIdWithChildrenAsync(rep.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.ChildrenSourceFormatNodes.Should().NotBeNull();
        result.ChildrenSourceFormatNodes.Should().BeEmpty();
    }
}