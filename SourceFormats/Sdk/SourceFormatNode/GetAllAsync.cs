namespace EncyclopediaGalactica.SourceFormats.Sdk.SourceFormatNode;

using Api;
using Dtos;
using Exceptions;
using Models.SourceFormatNode;

public partial class SourceFormatNodeSdk
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeGetAllResponseModel> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            HttpRequestMessage httpRequestMessage = PrepareGet(SourceFormatNode.GetAll);
            SourceFormatNodeGetAllResponseModel result = (SourceFormatNodeGetAllResponseModel)await _sdkCore
                .SendAsync<SourceFormatNodeGetAllResponseModel, SourceFormatNodeDto>(
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