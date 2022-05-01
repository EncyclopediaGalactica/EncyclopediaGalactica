namespace EncyclopediaGalactica.Sdk.Core;

using Interfaces;
using Model.Interfaces;
using Newtonsoft.Json;

public class SdkCore : ISdkCore
{
    private readonly HttpClient _httpClient;

    public SdkCore(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<TResponseModel> SendAsync<TResponseModel, TResponseModelPayload>(
        HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken = default)
        where TResponseModel : IResponseModel<TResponseModelPayload>
    {
        if (httpRequestMessage is null)
            throw new ArgumentNullException(nameof(httpRequestMessage));

        HttpResponseMessage response = await _httpClient.SendAsync(
                httpRequestMessage,
                cancellationToken)
            .ConfigureAwait(false);

        TResponseModel res = await CreateResponse<TResponseModel, TResponseModelPayload>(
                response,
                cancellationToken)
            .ConfigureAwait(false);

        return res;
    }

    private async Task<TResponseModel> CreateResponse<TResponseModel, TResponseModelPayload>(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default)
        where TResponseModel : IResponseModel<TResponseModelPayload>
    {
        TResponseModel result = Activator.CreateInstance<TResponseModel>();

        try
        {
            httpResponseMessage.EnsureSuccessStatusCode();
            TResponseModel? deserializedPayload = await DeserializeResponse<TResponseModel>(
                    httpResponseMessage,
                    cancellationToken)
                .ConfigureAwait(false);

            if (deserializedPayload is null)
                throw new Exception("Response is null.");

            return deserializedPayload;
        }
        catch (HttpRequestException httpRequestException)
        {
            result.IsOperationSuccessful = false;
            return result;
        }
    }

    private HttpRequestMessage PrepareHttpRequestMessage(StringContent stringContent, HttpMethod httpMethod, string url)
    {
        HttpRequestMessage message = new(httpMethod, url);
        message.Content = stringContent;
        return message;
    }

    private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        string content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        T? result = JsonConvert.DeserializeObject<T>(content);
        return result;
    }

    private StringContent? CreateStringContent<T>(IRequestModel<T> requestModel)
    {
        if (requestModel.Payload is null) return null;

        string jsonString = JsonConvert.SerializeObject(requestModel);
        StringContent content = new(jsonString);
        return content;
    }
}