namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Document;

using Api;
using Contracts.Input;
using Exceptions;
using Models;
using Models.Document;
using SourceFormatNode;

public partial class DocumentSdk
{
    /// <inheritdoc />
    public async Task<DocumentAddResponseModel> AddAsync(DocumentAddRequestModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(model.Payload);

            const string url = SourceFormats.Document.Route + SourceFormats.Document.Add;

            HttpRequestMessageBuilder<DocumentGraphqlInput> httpRequestMessageBuilder = new();
            HttpRequestMessage httpRequestMessage = httpRequestMessageBuilder
                .SetContent(model.Payload)
                .SetUri(url)
                .SetAcceptHeaders(model.AcceptHeaders)
                .SetHttpMethod(HttpMethod.Post)
                .Build();

            DocumentAddResponseModel responseModel = await _sdkCore
                .SendAsync<DocumentAddResponseModel, DocumentGraphqlInput>(
                    httpRequestMessage,
                    cancellationToken)
                .ConfigureAwait(false);

            return responseModel;
        }
        catch (Exception e)
        {
            string msg = $"Error happened while executing {nameof(SourceFormatNodeSdk)}.{nameof(AddAsync)}. " +
                         "For further information see inner exception.";
            throw new SdkException(msg, e);
        }
    }
}