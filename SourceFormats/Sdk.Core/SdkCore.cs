namespace EncyclopediaGalactica.SourceFormats.Sdk.Core;

using Interfaces;
using Newtonsoft.Json;

public class SdkCore : ISdkCore
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

    public async Task<TModel> SendAsync<TModel, TPayload>(
        HttpRequestMessage message,
        CancellationToken cancellationToken = default)
        where TPayload : new()
        where TModel : new()
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        HttpResponseMessage response = await _httpClient.SendAsync(
                message,
                cancellationToken)
            .ConfigureAwait(false);

        TModel res = await CreateResponse<TModel, TPayload>(response, cancellationToken).ConfigureAwait(false);


        return result;
    }

    private async Task<TModel> CreateResponse<TModel, TPayload>(
        HttpResponseMessage httpResponseMessage,
        CancellationToken cancellationToken = default)
        where TModel : new()
        where TPayload : new()
    {
        TModel result = new TModel();
        try
        {
            httpResponseMessage.EnsureSuccessStatusCode();
            TPayload deserializedPayload = await DeserializeResponse<TPayload>(
                    httpResponseMessage,
                    cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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