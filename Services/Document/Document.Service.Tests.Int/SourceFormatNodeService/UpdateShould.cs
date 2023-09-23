namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Dtos;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "DocumentService")]
public class UpdateShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_SuccessCode_AndUpdatedEntity()
    {
        // Arrange
        SourceFormatNodeDto dto = new()
        {
            Name = "asd"
        };
        SourceFormatNodeDto addResponseModel = await Sut.SourceFormatNode
            .AddAsync(dto);
        string updatedName = "asdasd";
        SourceFormatNodeDto updateTemplate = new()
        {
            Id = addResponseModel.Id,
            Name = updatedName
        };

        // Act
        SourceFormatNodeDto updateResponseModel = await Sut.SourceFormatNode
            .UpdateSourceFormatNodeAsync(updateTemplate);

        // Assert
        updateResponseModel.Should().NotBeNull();
        updateResponseModel.Id.Should().Be(updateTemplate.Id);
        updateResponseModel.Name.Should().Be(updateTemplate.Name);
    }

    [Fact]
    public async Task Throw_InvalidOperationException_WhenNoSuchEntityToBeUpdated()
    {
        // Arrange
        SourceFormatNodeDto updateTemplate = new()
        {
            Id = 204,
            Name = "asdasd"
        };

        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .UpdateSourceFormatNodeAsync(updateTemplate);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
}