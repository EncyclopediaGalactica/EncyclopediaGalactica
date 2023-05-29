namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeDto> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        _guards.IsNotEqual(id, 0);
        SourceFormatNode result = await _sourceFormatNodeRepository.GetByIdWithRootNodeAsync(id, cancellationToken)
            .ConfigureAwait(false);
        return _sourceFormatMappers.SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDto(result);
    }
}