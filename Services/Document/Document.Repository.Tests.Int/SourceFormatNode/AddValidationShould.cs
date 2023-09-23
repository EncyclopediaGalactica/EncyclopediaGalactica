namespace EncyclopediaGalactica.Services.Document.Repository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Entities;
using FluentAssertions;
using FluentValidation;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "Repository")]
public class AddValidationShould : BaseTest
{
    [Theory]
    [InlineData(1, "asd")]
    [InlineData(0, "as")]
    [InlineData(0, "")]
    [InlineData(0, null)]
    [InlineData(0, " ")]
    [InlineData(0, "  ")]
    [InlineData(0, "   ")]
    public void Throw_WhenInputIsInvalid(long id, string name)
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode();
        node.Id = id;
        node.Name = name;

        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.AddAsync(node).ConfigureAwait(false); };

        // Assert
        action.Should()
            .ThrowExactlyAsync<ValidationException>();
    }
}