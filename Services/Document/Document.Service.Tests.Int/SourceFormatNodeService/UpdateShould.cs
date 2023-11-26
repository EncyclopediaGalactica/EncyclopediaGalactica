namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class UpdateShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_SuccessCode_AndUpdatedEntity()
    {
        // Arrange
        SourceFormatNodeInput input = new()
        {
            Name = "asd"
        };
        SourceFormatNodeInput addResponseModel = await Sut.SourceFormatNode
            .AddAsync(input);
        string updatedName = "asdasd";
        SourceFormatNodeInput updateTemplate = new()
        {
            Id = addResponseModel.Id,
            Name = updatedName
        };

        // Act
        SourceFormatNodeInput updateResponseModel = await Sut.SourceFormatNode
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
        SourceFormatNodeInput updateTemplate = new()
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