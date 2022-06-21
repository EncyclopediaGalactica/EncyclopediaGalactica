namespace EncyclopediaGalactica.SourceFormats.E2E;

using System;
using System.Threading.Tasks;
using FluentAssertions;
using QA.Datasets;
using Sdk.Models;
using Sdk.Models.SourceFormatNode;
using Xunit;

public partial class SourceFormatNodeSdk_Should
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.AddValidationDataSet), MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_WhenTheUserTriesToBuildAnInvalidDataset(string name)
    {
        // Arrange & Act
        Action action = () =>
        {
            SourceFormatNodeAddRequestModel model = new SourceFormatNodeAddRequestModel.Builder()
                .SetName(name)
                .Build();
        };

        // Assert
        action.Should().ThrowExactly<SdkModelsException>();
    }
}