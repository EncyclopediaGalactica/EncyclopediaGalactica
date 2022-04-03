namespace Sdk.Models.Unit.Tests;

using System;
using EncyclopediaGalactica.SourceFormats.Sdk.Models;
using FluentAssertions;
using Xunit;

public class SourceFormatNodeAddModelShould
{
    [Fact]
    public void BuildObject()
    {
        // Arrange & Act
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetId(0)
            .SetName("asd")
            .Build();

        // Assert
        requestModel.Should().BeOfType<SourceFormatNodeAddRequestModel>();
        requestModel.Name.Should().Be("asd");
    }

    [Fact]
    public void Throw_WhenIdIsSetUp()
    {
        // Arrange & Act
        Action action = () =>
        {
            SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
                .SetId(100)
                .SetName("asd")
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }
}