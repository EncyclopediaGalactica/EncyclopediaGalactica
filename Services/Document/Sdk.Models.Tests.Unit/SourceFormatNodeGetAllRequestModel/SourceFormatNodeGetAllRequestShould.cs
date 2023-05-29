namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeGetAllRequestModel;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeGetAllRequestShould
{
    [Fact]
    public void Build_SourceFormatNodeGetAllRequestModel()
    {
        // Act
        SourceFormatNodeGetAllRequestModel requestModel = new SourceFormatNodeGetAllRequestModel.Builder()
            .Build();

        // Assert
        requestModel.Should().NotBeNull();
        requestModel.Should().BeOfType<SourceFormatNodeGetAllRequestModel>();
        requestModel.Payload.Should().BeNull();
        requestModel.AcceptHeaders.Should().NotBeNull();
        requestModel.AcceptHeaders.Should().BeOfType<List<MediaTypeWithQualityHeaderValue>>();
    }
}