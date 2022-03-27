namespace Sdk.Core;

using Exceptions;
using Interfaces;
using Newtonsoft.Json;

public class SdkCore : ISdkCore
{
    private readonly HttpClient _httpClient;

    public SdkCore(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public HttpRequestMessage PreparePost(object dto, string url)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException(nameof(url));

        StringContent stringContent = CreateStringContent(dto);
        HttpRequestMessage message = PrepareHttpRequestMessage(stringContent, HttpMethod.Post, url);
        return message;
    }

    private HttpRequestMessage PrepareHttpRequestMessage(StringContent stringContent, HttpMethod httpMethod, string url)
    {
        HttpRequestMessage message = new HttpRequestMessage(httpMethod, url);
        message.Content = stringContent;
        return message;
    }

    public async Task<T?> SendAsync<T>(HttpRequestMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            if (message is null)
                throw new ArgumentNullException(nameof(message));

            HttpResponseMessage response = await _httpClient.SendAsync(
                    message,
                    cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            T? result = await DeserializeResponse<T>(response, cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            // TODO: implement retry once the system has this capability
            string msg = $"Error happened while executing {nameof(SdkCore)}.{nameof(SendAsync)}. " +
                         "For further information see inner exception.";
            throw new SourceFormatsSdkCoreException(msg, e);
        }
    }

    private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        String content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        T? result = JsonConvert.DeserializeObject<T>(content);
        return result;
    }

    private StringContent CreateStringContent(object obj)
    {
        String jsonString = JsonConvert.SerializeObject(obj);
        StringContent content = new StringContent(jsonString);
        return content;
    }
}