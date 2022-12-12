namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using Entities;
using FluentAssertions;
using Interfaces.SourceFormatNode;
using Sdk.Models.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class GetByIdShould : BaseTest
{
    [Fact]
    public async Task Return_TheDesignatedEntity()
    {
        // Arrange
        SourceFormatNodeDto node = new SourceFormatNodeDto { Name = "asdasd" };
        SourceFormatNodeDto entity = await _sourceFormatsService.SourceFormatNode.AddAsync(node)
            .ConfigureAwait(false);

        // Act
        SourceFormatNodeDto result = await _sourceFormatsService.SourceFormatNode
            .GetByIdAsync(entity.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchEntity()
    {
        // Act
        Func<Task> task = async () =>
        {
            await _sourceFormatsService.SourceFormatNode.GetByIdAsync(100).ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}