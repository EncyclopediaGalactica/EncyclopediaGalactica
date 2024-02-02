namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class GetAllShould : BaseTest
{
    [Fact]
    public async Task ReturnResponseModel_AllEntities_WhenThereAreEntitiesInTheDb()
    {
        // Arrange
        string name = "asdasd";
        SourceFormatNodeInput input = new()
        {
            Name = name
        };

        await Sut.SourceFormatNode
            .AddAsync(input);

        // Act
        List<SourceFormatNodeInput> result = await Sut.SourceFormatNode
            .GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().BeGreaterThan(0);
        SourceFormatNodeInput elem = result.ElementAt(0);
        elem.Name.Should().Be(name);
    }

    [Fact]
    public async Task ReturnsResponseModel_EmptyList_WhenThereAreNoEntitiesInTheDb()
    {
        // Act
        List<SourceFormatNodeInput> result = await Sut.SourceFormatNode.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(0);
    }
}