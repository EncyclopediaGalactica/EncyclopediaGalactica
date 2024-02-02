namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task DeleteAsync(
        SourceFormatNodeInput input,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        _guards.IsNotEqual(input.Id, 0);

        await _sourceFormatNodeRepository.DeleteAsync(input.Id, cancellationToken).ConfigureAwait(false);
    }
}