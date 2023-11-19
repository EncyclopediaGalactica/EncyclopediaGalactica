namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeInputContract> AddChildToParentAsync(
        SourceFormatNodeInputContract childInputContract,
        SourceFormatNodeInputContract parentInputContract,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(childInputContract);
        ArgumentNullException.ThrowIfNull(parentInputContract);
        _guards.IsNotEqual(childInputContract.Id, 0);
        _guards.IsNotEqual(parentInputContract.Id, 0);
        _guards.IsNotEqual(parentInputContract.Id, childInputContract.Id);

        SourceFormatNode rootNode = await _sourceFormatNodeRepository.GetByIdWithRootNodeAsync(
                parentInputContract.Id,
                cancellationToken)
            .ConfigureAwait(false);
        SourceFormatNode resultNode = await _sourceFormatNodeRepository.AddChildNodeAsync(
                childInputContract.Id,
                parentInputContract.Id,
                rootNode.Id,
                cancellationToken)
            .ConfigureAwait(false);
        return _sourceFormatMappers.SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(resultNode);
    }
}