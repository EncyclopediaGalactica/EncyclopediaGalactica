namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Tests.Unit.SourceFormatNodeAddRequestModel;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Services.Document.Tests.Datasets;
using SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeAddModelValidationShould
{
    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.AddValidationDataSet), MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_WhenInputDataIsInvalid(string name)
    {
        // Arrange && Act
        Action action = () =>
        {
            SourceFormatNodeAddRequestModel model = new SourceFormatNodeAddRequestModel.Builder()
                .SetName(name)
                .Build();
        };

        // Assert
        action.Should().Throw<SdkModelsException>();
    }
}