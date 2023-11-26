namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<List<SourceFormatNodeInput>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        List<SourceFormatNode> sourceFormatNodes = await _sourceFormatNodeRepository
            .GetAllAsync(cancellationToken)
            .ConfigureAwait(false);
        return _sourceFormatMappers.SourceFormatNodeMappers
            .MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(sourceFormatNodes);
    }
}