namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Unit.Tests.DocumentService;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Document;
using FluentAssertions;
using Mappers;
using Mappers.Interfaces;
using Moq;
using Utils.GuardsService;
using Utils.GuardsService.Interfaces;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DocumentService_Should
{
    public static IEnumerable<object[]> ThrowArgumentNullException_WhenInjected_IsNull_Data = new List<object[]>
    {
        new[] { null, new DocumentService(new GuardsService(), new Mock<ISourceFormatMappers>().Object) },
        new[] { new SourceFormatMappers(new Mock<ISourceFormatNodeMappers>().Object), null },
        new object[] { null, null }
    };

    [Theory]
    [MemberData(nameof(ThrowArgumentNullException_WhenInjected_IsNull_Data))]
    public void ThrowArgumentNullException_WhenInjected_IsNull(
        IGuardsService guardsService,
        ISourceFormatMappers mappers)
    {
        // Arrange && Act
        Action action = () => { new DocumentService(guardsService, mappers); };

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }
}