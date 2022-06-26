namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.Unit.Tests.SourceFormatNodeDeleteRequestModel;

using System;
using FluentAssertions;
using SourceFormatNode;
using Xunit;

public class SourceFormatNodeDeleteRequestModelValidationShould
{
    [Fact]
    public void Throw_WhenIdIsEqualToZero()
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeDeleteRequestModel model = new SourceFormatNodeDeleteRequestModel.Builder()
                .SetId(0)
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }
}