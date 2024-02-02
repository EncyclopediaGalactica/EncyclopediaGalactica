namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetByIdShould : BaseTest
{
    [Fact]
    public async Task Return_TheDesignatedEntity()
    {
        // Arrange
        SourceFormatNodeInput node = new SourceFormatNodeInput { Name = "asdasd" };
        SourceFormatNodeInput entity = await Sut.SourceFormatNode.AddAsync(node);

        // Act
        SourceFormatNodeInput result = await Sut.SourceFormatNode
            .GetByIdAsync(entity.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchEntity()
    {
        // Act
        Func<Task> task = async () => { await Sut.SourceFormatNode.GetByIdAsync(100); };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}