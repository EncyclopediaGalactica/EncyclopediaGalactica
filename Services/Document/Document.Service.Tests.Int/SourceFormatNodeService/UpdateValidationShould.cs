namespace EncyclopediaGalactica.Services.Document.Service.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Base;
using Contracts.Input;
using FluentAssertions;
using FluentValidation;
using Services.Document.Tests.Datasets;
using Utils.GuardsService.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
public class UpdateValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_ArgumentNullException_WhenInputIsNull()
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .UpdateSourceFormatNodeAsync(null);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Throw_GuardsServiceValueShouldNotBeEqualToException_WhenInputIdIsZero()
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .UpdateSourceFormatNodeAsync(new SourceFormatNodeInput { Id = 0 });
        };

        // Assert
        await task.Should().ThrowExactlyAsync<GuardsServiceValueShouldNotBeEqualToException>();
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.UpdateValidationDataSet_Without_IdIsZero),
        MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_ValidationError_WhenInputIsInvalid(int id, string name)
    {
        // Act
        SourceFormatNodeInput input = new()
        {
            Id = id,
            Name = name
        };
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .UpdateSourceFormatNodeAsync(input);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ValidationException>();
    }
}