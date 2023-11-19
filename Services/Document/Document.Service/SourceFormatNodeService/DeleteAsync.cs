namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task DeleteAsync(
        SourceFormatNodeInputContract inputContract,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(inputContract);
        _guards.IsNotEqual(inputContract.Id, 0);

        await _sourceFormatNodeRepository.DeleteAsync(inputContract.Id, cancellationToken).ConfigureAwait(false);
    }
}