namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddShould : BaseTest
{
    [Fact]
    public async Task ReturnsResponseModel_SuccessCode_AndWithOperationResult()
    {
        // Arrange
        string name = "asd";
        SourceFormatNodeInput input = new()
        {
            Name = name
        };

        // Act
        SourceFormatNodeInput result = await Sut
            .SourceFormatNode
            .AddAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeNull();
        result.Id.Should().NotBe(0);
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(name);
    }
}