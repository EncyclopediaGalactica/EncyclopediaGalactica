namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeAddChildToParentRequestModel;

using System.Collections.Generic;
using System.Net.Http.Headers;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;
using FluentAssertions;
using Xunit;

public class SourceFormatNodeAddChildToParentRequestModelShould
{
    [Fact]
    public void Build_RequestModel()
    {
        // Act
        SourceFormatNodeAddChildToParentRequestModel requestModel =
            new SourceFormatNodeAddChildToParentRequestModel.Builder()
                .SetChildrenNodeId(1)
                .SetParentNodeId(2)
                .Build();

        // Assert
        requestModel.Should().NotBeNull();
        requestModel.Should().BeOfType<SourceFormatNodeAddChildToParentRequestModel>();
        requestModel.Payload.Should().NotBeNull();
        requestModel.Payload.Should().BeOfType<SourceFormatNodeDto>();
        requestModel.Payload.Id.Should().Be(1);
        requestModel.Payload.ParentNodeId.Should().Be(2);
        requestModel.AcceptHeaders.Should().NotBeNull();
        requestModel.AcceptHeaders.Should().BeOfType<List<MediaTypeWithQualityHeaderValue>>();
    }
}