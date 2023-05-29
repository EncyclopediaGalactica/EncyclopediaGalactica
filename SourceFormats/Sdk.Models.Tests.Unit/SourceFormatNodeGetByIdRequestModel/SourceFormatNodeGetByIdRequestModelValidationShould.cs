namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeGetByIdRequestModel;

using System;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeGetByIdRequestModelValidationShould
{
    [Fact]
    public void Throw_WhenARequestModelIsBuilt_WithoutId()
    {
        // Arrange
        Action action = () =>
        {
            SourceFormatNodeGetByIdRequestModel model = new SourceFormatNodeGetByIdRequestModel.Builder()
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }

    [Fact]
    public void Throw_WhenARequestModelIsBuilt_WithIdZero()
    {
        // Arrange
        Action action = () =>
        {
            SourceFormatNodeGetByIdRequestModel model = new SourceFormatNodeGetByIdRequestModel.Builder()
                .SetId(0)
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }
}