namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeAddRequestModel;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
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