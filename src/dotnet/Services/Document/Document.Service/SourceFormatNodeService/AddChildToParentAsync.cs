namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeInput> AddChildToParentAsync(
        SourceFormatNodeInput childInput,
        SourceFormatNodeInput parentInput,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(childInput);
        ArgumentNullException.ThrowIfNull(parentInput);
        _guards.IsNotEqual(childInput.Id, 0);
        _guards.IsNotEqual(parentInput.Id, 0);
        _guards.IsNotEqual(parentInput.Id, childInput.Id);

        SourceFormatNode rootNode = await _sourceFormatNodeRepository.GetByIdWithRootNodeAsync(
                parentInput.Id,
                cancellationToken)
            .ConfigureAwait(false);
        SourceFormatNode resultNode = await _sourceFormatNodeRepository.AddChildNodeAsync(
                childInput.Id,
                parentInput.Id,
                rootNode.Id,
                cancellationToken)
            .ConfigureAwait(false);
        return _sourceFormatMappers.SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(resultNode);
    }
}