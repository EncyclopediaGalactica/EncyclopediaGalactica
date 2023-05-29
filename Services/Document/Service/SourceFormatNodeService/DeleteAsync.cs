namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.SourceFormatNodeService;

using Dtos;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task DeleteAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        _guards.IsNotEqual(dto.Id, 0);

        await _sourceFormatNodeRepository.DeleteAsync(dto.Id, cancellationToken).ConfigureAwait(false);
    }
}