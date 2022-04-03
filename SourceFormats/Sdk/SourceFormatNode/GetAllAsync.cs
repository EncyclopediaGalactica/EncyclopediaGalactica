namespace EncyclopediaGalactica.SourceFormats.Sdk.SourceFormatNode;

using Api;
using Dtos;
using Exceptions;

public partial class SourceFormatNodeSdk
{
    /// <inheritdoc />
    public async Task<List<SourceFormatNodeDto>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            HttpRequestMessage httpRequestMessage = PrepareGet(SourceFormatNode.GetAll);
            List<SourceFormatNodeDto>? result = await _sdkCore.SendAsync<List<SourceFormatNodeDto>>(
                    httpRequestMessage,
                    cancellationToken)
                .ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            string msg = $"Error happened while executing {nameof(SourceFormatNodeSdk)}.{nameof(GetAllAsync)}. " +
                         "For further information see inner exception.";
            throw new SdkException(msg, e);
        }
    }
}