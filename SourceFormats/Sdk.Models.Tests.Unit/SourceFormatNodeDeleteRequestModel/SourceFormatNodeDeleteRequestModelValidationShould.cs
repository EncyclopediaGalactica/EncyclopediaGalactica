namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeDeleteRequestModel;

using System;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
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