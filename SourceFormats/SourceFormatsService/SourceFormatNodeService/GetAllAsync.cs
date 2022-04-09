namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using Sdk.Models.SourceFormatNode;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeGetAllResponseModel> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // TODO: figure out how caching can play a role here, especially caching strategies
            List<SourceFormatNode> sourceFormatNodes = await _sourceFormatNodeRepository
                .GetAllAsync(cancellationToken)
                .ConfigureAwait(false);
            List<SourceFormatNodeDto> mapped = _sourceFormatMappers.SourceFormatNodeMappers
                .MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(sourceFormatNodes);
            SourceFormatNodeGetAllResponseModel responseModel = new SourceFormatNodeGetAllResponseModel
            {
                Result = mapped
            };
            return responseModel;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}