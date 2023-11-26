namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeInput> GetByIdAsync(
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