namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.Unit.Tests.SourceFormatNodeAddChildToParentRequestModel;

using System;
using FluentAssertions;
using SourceFormatNode;
using Xunit;

public class SourceFormatNodeAddChildToParentRequestModelValidationShould
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 0)]
    [InlineData(0, 0, 1)]
    public void Throw_WhenUserTriesToBuildRequestModel_WithInvalidInput(
        long childId, long parentId, long rootId)
    {
        // Act
        Action action = () =>
        {
            SourceFormatNodeAddChildToParentRequestModel requestModel = new
                    SourceFormatNodeAddChildToParentRequestModel.Builder()
                .SetChildrenNodeId(childId)
                .SetParentNodeId(parentId)
                .SetRootNodeId(rootId)
                .Build();
        };

        // Assert
        action.Should().Throw<SdkModelsException>();
    }
}