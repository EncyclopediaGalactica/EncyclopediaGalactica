namespace EncyclopediaGalactica.SourceFormats.Sdk.Core;

using Models;
using Newtonsoft.Json;

public class SdkCore
{
    private readonly HttpClient _httpClient;

    public SdkCore(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public HttpRequestMessage PreparePost(object obj, string url)
    {
        if (obj is null)
            throw new ArgumentNullException(nameof(obj));
        if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException(nameof(url));

        StringContent stringContent = CreateStringContent(obj);
        HttpRequestMessage message = PrepareHttpRequestMessage(stringContent, HttpMethod.Post, url);
        return message;
    }

    public async Task<IResponseModel<TResponseModelPayload>> SendAsync<TResponseModelPayload>(
        HttpRequestMessage message,
        CancellationToken cancellationToken = default)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        HttpResponseMessage response = await _httpClient.SendAsync(
                message,
                cancellationToken)
            .ConfigureAwait(false);

        IResponseModel<TResponseModelPayload> res = await CreateResponse<TResponseModelPayload>(
                response,
                cancellationToken)
            .ConfigureAwait(false);

        return res;
    }

    private async Task<IResponseModel<TResponseModelPayload>> CreateResponse<TResponseModelPayload>(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default)
    {
        IResponseModel<TResponseModelPayload> result =
            Activator.CreateInstance<IResponseModel<TResponseModelPayload>>();

        try
        {
            httpResponseMessage.EnsureSuccessStatusCode();
            TResponseModelPayload deserializedPayload = await DeserializeResponse<TResponseModelPayload>(
                    httpResponseMessage,
                    cancellationToken)
                .ConfigureAwait(false);
            result.HttpStatusCode = (int)httpResponseMessage.StatusCode;
            result.Result = deserializedPayload;
            result.IsOperationSuccessful = true;
            return result;
        }
        catch (HttpRequestException httpRequestException)
        {
            string msg = $"Error happened.";
            result.Message = msg;
            result.HttpStatusCode = (int)httpResponseMessage.StatusCode;
            result.IsOperationSuccessful = false;
            return result;
        }
        catch (Exception e)
        {
            // something bad
            throw;
        }
    }

    private HttpRequestMessage PrepareHttpRequestMessage(StringContent stringContent, HttpMethod httpMethod, string url)
    {
        HttpRequestMessage message = new HttpRequestMessage(httpMethod, url);
        message.Content = stringContent;
        return message;
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        String content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        T result = JsonConvert.DeserializeObject<T>(content);
        return result;
    }

    private StringContent CreateStringContent(object obj)
    {
        String jsonString = JsonConvert.SerializeObject(obj);
        StringContent content = new StringContent(jsonString);
        return content;
    }
}