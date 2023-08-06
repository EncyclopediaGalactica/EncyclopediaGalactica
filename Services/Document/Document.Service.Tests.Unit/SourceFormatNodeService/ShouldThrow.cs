namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Tests.Unit.SourceFormatNodeService;

using System.Threading.Tasks;
using Xunit;

[Trait("Category", "DocumentService")]
public class ShouldThrow
{
    [Fact]
    public async Task SourceFormatNodeServiceInputValidationException_WhenInputValidationFails()
    {
        // Mock<IValidator<SourceFormatNodeDto>> validatorMock = new Mock<IValidator<SourceFormatNodeDto>>()
        //     .Setup(f => f.ValidateAsync(It.IsAny<SourceFormatNodeDto>()))
        //     .Throws<ValidationException>();
        // ISourceFormatNodeService nodeService = new SourceFormatNodeService();
        // ISourceFormatsService service = new IAM.Service();
    }

    [Fact]
    public async Task SourceFormatNodeServiceException_WhenMapperFails()
    {
    }

    [Fact]
    public async Task SourceFormatNodeServiceException_WhenRepositoryOperationFails()
    {
    }

    [Fact]
    public async Task SourceFormatNodeServiceException_WhenCacheOperationFails()
    {
    }
}