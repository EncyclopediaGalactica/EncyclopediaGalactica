namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.Unit.Tests.SourceFormatNodeAddRequestModel;

using Dtos;
using FluentAssertions;
using SourceFormatNode;
using Xunit;

public class SourceFormatNodeAddModelShould
{
    [Fact]
    public void BuildObject()
    {
        // Arrange & Act
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName("asd")
            .Build();

        // Assert
        requestModel.Should().BeOfType<SourceFormatNodeAddRequestModel>();
        requestModel.Payload.Should().BeOfType<SourceFormatNodeDto>();
    }
}