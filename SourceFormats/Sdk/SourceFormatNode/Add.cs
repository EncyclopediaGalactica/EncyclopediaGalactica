namespace EncyclopediaGalactica.SourceFormats.Sdk.SourceFormatNode;

using Api;
using Exceptions;
using Models;

public partial class SourceFormatNodeSdk
{
    public async Task<SourceFormatNodeAddResponseModel> AddAsync(
        SourceFormatNodeAddRequestModel addRequestModel,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (addRequestModel is null)
                throw new ArgumentNullException(nameof(addRequestModel));

            const string url = SourceFormatNode.Route + SourceFormatNode.Add;
            HttpRequestMessage message = _sdkCore.PreparePost(addRequestModel.Payload, url);

            SourceFormatNodeAddResponseModel response = await _sdkCore.SendAsync<SourceFormatNodeAddResponseModel>(
                    message,
                    cancellationToken)
                .ConfigureAwait(false);
            return response;
        }
        catch (Exception e)
        {
            string msg = $"Error happened while executing {nameof(SourceFormatNodeSdk)}.{nameof(AddAsync)}. " +
                         "For further information see inner exception.";
            throw new SdkException(msg, e);
        }
    }
}