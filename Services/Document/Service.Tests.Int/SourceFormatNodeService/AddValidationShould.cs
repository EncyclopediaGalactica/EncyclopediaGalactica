namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Int.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EncyclopediaGalactica.Services.Document.Dtos;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Document.Tests.Datasets;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_ArgumentNullException_WhenInputIsNull()
    {
        // Act
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddAsync(null!)
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(SourceFormatNodeDatasets.AddValidationDataSet), MemberType = typeof(SourceFormatNodeDatasets))]
    public async Task Throw_ValidationException_WhenInputIsInvalid(
        string name)
    {
        // Act
        SourceFormatNodeDto dto = new() { Name = name };
        Func<Task> task = async () =>
        {
            await Sut.SourceFormatNode
                .AddAsync(dto)
                .ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<ValidationException>();
    }

    [Fact]
    public async Task Throw_DbUpdateException_WhenNameUniquenessIsViolated()
    {
        // Arrange
        string name = "asdasd";
        SourceFormatNodeDto dto = new()
        {
            Name = name
        };
        await Sut
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Act
        Func<Task> task = async () =>
        {
            await Sut
                .SourceFormatNode
                .AddAsync(dto).ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<DbUpdateException>();
    }
}