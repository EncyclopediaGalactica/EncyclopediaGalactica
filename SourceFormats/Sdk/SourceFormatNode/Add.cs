namespace Sdk.SourceFormatNode;

using EncyclopediaGalactica.SourceFormats.Api;
using EncyclopediaGalactica.SourceFormats.Dtos;
using Exceptions;

public partial class SourceFormatNodeSdk
{
    public async Task<SourceFormatNodeDto?> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            const string url = SourceFormatNode.Route + SourceFormatNode.Add;
            HttpRequestMessage message = _sdkCore.PreparePost(dto, url);

            SourceFormatNodeDto? result = await _sdkCore.SendAsync<SourceFormatNodeDto>(
                    message,
                    cancellationToken)
                .ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            string msg = $"Error happened while executing {nameof(SourceFormatNodeSdk)}.{nameof(AddAsync)}. " +
                         "For further information see inner exception.";
            throw new SdkException(msg, e);
        }
    }
}