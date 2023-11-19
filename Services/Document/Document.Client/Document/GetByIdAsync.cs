namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Document;

using Api;
using Contracts.Input;
using Models;
using Models.Document;

public partial class DocumentSdk
{
    /// <inheritdoc />
    public async Task<DocumentGetByIdResponseModel> GetByIdAsync(
        DocumentGetByIdRequestModel requestModel,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(requestModel);
        ArgumentNullException.ThrowIfNull(requestModel.Payload);

        string uri = SourceFormats.Document.Route
                     + SourceFormats.Document.GetById
                     + $"/{requestModel.Payload.Id}";

        HttpRequestMessageBuilder<DocumentGraphqlInput> builder =
            new HttpRequestMessageBuilder<DocumentGraphqlInput>();
        HttpRequestMessage httpRequestMessage = builder
            .SetContent(requestModel.Payload)
            .SetAcceptHeaders(requestModel.AcceptHeaders)
            .SetHttpMethod(HttpMethod.Get)
            .SetUri(uri)
            .Build();

        DocumentGetByIdResponseModel resultModel = await _sdkCore
            .SendAsync<DocumentGetByIdResponseModel, DocumentGraphqlInput>(
                httpRequestMessage,
                cancellationToken)
            .ConfigureAwait(false);

        return resultModel;
    }
}