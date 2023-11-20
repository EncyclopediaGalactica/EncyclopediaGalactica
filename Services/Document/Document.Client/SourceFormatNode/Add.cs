namespace EncyclopediaGalactica.Services.Document.Sdk.Client.SourceFormatNode;

using Api;
using Contracts.Input;
using Exceptions;
using Models;
using Models.SourceFormatNode;

public partial class SourceFormatNodeSdk
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeAddResponseModel> AddAsync(
        SourceFormatNodeAddRequestModel addRequestModel,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(addRequestModel);
            ArgumentNullException.ThrowIfNull(addRequestModel.Payload);

            const string url = SourceFormats.SourceFormatNode.Route + SourceFormats.SourceFormatNode.Add;

            HttpRequestMessageBuilder<SourceFormatNodeInput?> httpRequestMessageBuilder = new();
            HttpRequestMessage httpRequestMessage = httpRequestMessageBuilder
                .SetContent(addRequestModel.Payload)
                .SetUri(url)
                .SetAcceptHeaders(addRequestModel.AcceptHeaders)
                .SetHttpMethod(HttpMethod.Post)
                .Build();

            SourceFormatNodeAddResponseModel response = await _sdkCore
                .SendAsync<SourceFormatNodeAddResponseModel, SourceFormatNodeInput>(
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