namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
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
        SourceFormatNodeInputContract inputContract = new()
        {
            Name = "asd"
        };
        SourceFormatNodeInputContract addResponseModel = await Sut.SourceFormatNode
            .AddAsync(inputContract);
        string updatedName = "asdasd";
        SourceFormatNodeInputContract updateTemplate = new()
        {
            Id = addResponseModel.Id,
            Name = updatedName
        };

        // Act
        SourceFormatNodeInputContract updateResponseModel = await Sut.SourceFormatNode
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
        SourceFormatNodeInputContract updateTemplate = new()
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