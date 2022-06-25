namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Int.Tests.SourceFormatNodeService;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

[ExcludeFromCodeCoverage]
[Collection("SourceFormatServiceDatabaseOperationCollection")]
public class DeleteValidationShould : BaseTest
{
    [Fact]
    public async Task Returns_ValidationErrorResult_WhenInputIsNull()
    {
    }

    [Fact]
    public async Task Returns_ValidationErrorResult_WhenInputIsInvalid()
    {
    }
}