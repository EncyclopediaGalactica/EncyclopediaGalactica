namespace EncyclopediaGalactica.SourceFormats.Sdk.SourceFormatNode;

using Api;
using Dtos;
using Exceptions;
using Models;
using Models.SourceFormatNode;

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
            if (addRequestModel.Payload is null)
                throw new ArgumentNullException(nameof(addRequestModel.Payload));

            const string url = SourceFormatNode.Route + SourceFormatNode.Add;

            HttpRequestMessageBuilder<SourceFormatNodeDto?> httpRequestMessageBuilder =
                new HttpRequestMessageBuilder<SourceFormatNodeDto?>();
            HttpRequestMessage httpRequestMessage = httpRequestMessageBuilder
                .SetContent(addRequestModel.Payload)
                .SetUri(url)
                .SetAcceptHeaders(addRequestModel.AcceptHeaders)
                .SetHttpMethod(HttpMethod.Post)
                .Build();

            SourceFormatNodeAddResponseModel response = await _sdkCore
                .SendAsync<SourceFormatNodeAddResponseModel, SourceFormatNodeDto>(
                    httpRequestMessage,
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