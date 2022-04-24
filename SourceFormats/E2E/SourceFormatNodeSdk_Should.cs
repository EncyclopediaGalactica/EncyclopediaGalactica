namespace EncyclopediaGalactica.SourceFormats.E2E;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using FluentAssertions;
using QA.Datasets;
using Sdk.Models;
using Sdk.Models.SourceFormatNode;
using Xunit;

[ExcludeFromCodeCoverage]
public class SourceFormatNodeSdk_Should : TestBase
{
    public SourceFormatNodeSdk_Should(SourceFormatWebApplicationFactory<Program> webApplicationFactory) : base(
        webApplicationFactory)
    {
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.ValidationDataSet), MemberType = typeof(SourceFormatNodeDatasets))]
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

    [Fact]
    public async Task CreatesSourceFormatNode()
    {
        // Arrange
        string name = "asd";
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName(name)
            .Build();

        // Act
        SourceFormatNodeAddResponseModel responseModel = await SourceFormatsSdk.SourceFormatNode.AddAsync(requestModel)
            .ConfigureAwait(false);

        // Assert
        responseModel.Should().NotBeNull();
        responseModel.IsOperationSuccessful.Should().BeTrue();
        responseModel.HttpStatusCode.Should().Be(201);
        responseModel.Message.Should().BeNull();
        responseModel.Result.Should().NotBeNull();
        responseModel.Result.Should().BeOfType<SourceFormatNodeDto>();
        responseModel.Result.Id.Should().BeGreaterThan(0);
        responseModel.Result.Name.Should().Be(name);
    }
}