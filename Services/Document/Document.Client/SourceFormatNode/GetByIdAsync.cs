namespace EncyclopediaGalactica.Services.Document.Sdk.Client.SourceFormatNode;

using Api;
using Contracts.Input;
using Exceptions;
using Models;
using Models.SourceFormatNode;

public partial class SourceFormatNodeSdk
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeGetByIdResponseModel> GetByIdAsync(
        SourceFormatNodeGetByIdRequestModel getByIdRequestModel,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(getByIdRequestModel);
            string uri = SourceFormats.SourceFormatNode.Route
                         + SourceFormats.SourceFormatNode.GetById
                         + $"/{getByIdRequestModel.Payload.Id}";
            HttpRequestMessageBuilder<SourceFormatNodeInput> httpRequestMessageBuilder =
                new HttpRequestMessageBuilder<SourceFormatNodeInput>();
            HttpRequestMessage httpRequestMessage = httpRequestMessageBuilder
                .SetUri(uri)
                .SetHttpMethod(HttpMethod.Get)
                .SetAcceptHeaders(getByIdRequestModel.AcceptHeaders)
                .Build();

            SourceFormatNodeGetByIdResponseModel result = await _sdkCore
                .SendAsync<SourceFormatNodeGetByIdResponseModel, SourceFormatNodeInput>(
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