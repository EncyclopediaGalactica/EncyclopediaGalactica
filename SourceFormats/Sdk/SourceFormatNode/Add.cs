namespace Sdk.SourceFormatNode;

using EncyclopediaGalactica.SourceFormats.Api;
using EncyclopediaGalactica.SourceFormats.Dtos;

public partial class SourceFormatNodeSdk
{
    public async Task<SourceFormatNodeDto?> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        HttpRequestMessage message = _sdkCore.PreparePost(dto, SourceFormatNode.Add);

        SourceFormatNodeDto? result = await _sdkCore.SendAsync<SourceFormatNodeDto>(
                message,
                cancellationToken)
            .ConfigureAwait(false);
        return result;
    }
}