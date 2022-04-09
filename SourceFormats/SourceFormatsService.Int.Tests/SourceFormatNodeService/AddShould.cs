namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Sdk.Models.SourceFormatNode;

[ExcludeFromCodeCoverage]
public class AddShould : BaseTest
{
    public async Task Add_AnItem_AndReturnIt()
    {
        // Arrange
        string name = "asdasd";
        SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel.Builder()
            .SetName(name)
            .Build();

        // Act
        SourceFormatNodeAddResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(requestModel).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().NotBeNull();
        result.Result.Id.Should().NotBe(0);
        result.Result.Id.Should().BeGreaterThan(0);
        result.Result.Name.Should().Be(name);
    }
}