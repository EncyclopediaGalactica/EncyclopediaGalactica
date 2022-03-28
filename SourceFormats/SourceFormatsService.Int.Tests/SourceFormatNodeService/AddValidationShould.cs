namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dtos;
using Exceptions;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddValidationShould : BaseTest
{
    [Fact]
    public async Task Throw_WhenInputIsNull()
    {
        // Act
        Func<Task> task = async () =>
        {
            await _sourceFormatsService
                .SourceFormatNode
                .AddAsync(null!).ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<SourceFormatNodeServiceInputValidationException>()
            .WithInnerExceptionExactly<SourceFormatNodeServiceInputValidationException, ArgumentNullException>()
            .ConfigureAwait(false);
    }

    [Theory]
    [InlineData(1, "asd")]
    [InlineData(0, null)]
    [InlineData(0, "a")]
    [InlineData(0, "")]
    [InlineData(0, "   ")]
    public async Task Throw_WhenInputIsInvalid(long id, string name)
    {
        // Arrange
        SourceFormatNodeDto dto = new SourceFormatNodeDto();
        dto.Id = id;
        dto.Name = name;

        // Act
        Func<Task> task = async () =>
        {
            await _sourceFormatsService
                .SourceFormatNode
                .AddAsync(dto).ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<SourceFormatNodeServiceInputValidationException>()
            .WithInnerExceptionExactly<SourceFormatNodeServiceInputValidationException, ValidationException>()
            .ConfigureAwait(false);
    }

    [Fact]
    public async Task Throw_WhenNameUniquenessIsViolated()
    {
        // Arrange
        SourceFormatNodeDto dto = new SourceFormatNodeDto();
        dto.Name = "asd";
        SourceFormatNodeDto dtoResult = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto).ConfigureAwait(false);

        // Act
        // await _sourceFormatNodeService.AddAsync(dto).ConfigureAwait(false);
        Func<Task> task = async () =>
        {
            await _sourceFormatsService
                .SourceFormatNode
                .AddAsync(dto).ConfigureAwait(false);
        };

        // Assert
        await task.Should().ThrowExactlyAsync<SourceFormatNodeServiceInputValidationException>()
            .WithInnerExceptionExactly<SourceFormatNodeServiceInputValidationException,
                SourceFormatNodeRepositoryException>()
            .WithInnerExceptionExactly<SourceFormatNodeRepositoryException, DbUpdateException>()
            .ConfigureAwait(false);
    }
}