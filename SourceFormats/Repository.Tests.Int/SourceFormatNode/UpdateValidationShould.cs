namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Tests.Int.SourceFormatNode;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Services.Document.Entities;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Exceptions;
using FluentAssertions;
using FluentValidation;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatNode Repository Collection")]
public class UpdateValidationShould : BaseTest
{
    [Theory]
    [InlineData(0, "asd")]
    [InlineData(1, "as")]
    [InlineData(1, "")]
    [InlineData(1, null)]
    [InlineData(1, " ")]
    [InlineData(1, "  ")]
    [InlineData(1, "   ")]
    public void Throw_WhenInputIsInvalid(long id, string name)
    {
        // Arrange
        SourceFormatNode node = new SourceFormatNode();
        node.Id = id;
        node.Name = name;

        // Act
        Func<Task> action = async () => { await Sut.SourceFormatNodes.UpdateAsync(node).ConfigureAwait(false); };

        // Assert
        action.Should()
            .ThrowExactlyAsync<SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, ValidationException>()
            .ConfigureAwait(false);
    }
}