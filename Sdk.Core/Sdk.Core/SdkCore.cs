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
        where TResponseModel : IResponseModel<TResponseModelPayload>, new()
    {
        ArgumentNullException.ThrowIfNull(httpRequestMessage);

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
        where TResponseModel : IResponseModel<TResponseModelPayload>, new()
    {
        TResponseModel result = Activator.CreateInstance<TResponseModel>();
        TResponseModel res = new();

        try
        {
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

    private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        string content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        T? result = JsonConvert.DeserializeObject<T>(content);
        return result;
    }
}