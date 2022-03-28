namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<List<SourceFormatNodeDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // TODO: figure out how caching can play a role here, especially caching strategies
            List<SourceFormatNode> sourceFormatNodes = await _sourceFormatNodeRepository
                .GetAllAsync(cancellationToken)
                .ConfigureAwait(false);
            List<SourceFormatNodeDto> mapped = _sourceFormatMappers.SourceFormatNodeMappers
                .MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(sourceFormatNodes);
            return mapped;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}