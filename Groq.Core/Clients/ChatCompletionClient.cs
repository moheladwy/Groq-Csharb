using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Groq.Core.Models;
using Groq.Core.Settings;

namespace Groq.Core.Clients;

/// <summary>
///     A client for interacting with the Groq API's chat completion functionality.
/// </summary>
/// <remarks>
///     This class provides methods to execute chat completion requests and receive responses.
///     It includes both synchronous and streaming support for responses. The client uses an
///     HttpClient instance for communicating with the Groq API, with authentication handled
///     via an API key provided during initialization. Ensure any HttpClient provided is properly
///     configured for network communication with the Groq API.
/// </remarks>
public sealed class ChatCompletionClient
{
    /// <summary>The HTTP client used for making API requests.</summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    ///     Initializes a new instance of the ChatCompletionClient with a provided HttpClient.
    /// </summary>
    /// <param name="httpClient">The HttpClient instance to use for API requests.</param>
    public ChatCompletionClient(HttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    ///     Retrieves a list of available models from the Groq API.
    /// </summary>
    /// <returns>
    ///     A <c>ModelListResponse</c> containing the list of models.
    /// </returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    public async Task<ModelListResponse?> ListModelsAsync()
    {
        var response = await _httpClient.GetAsync(Endpoints.GetAllModelsEndpoint);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<ModelListResponse>(responseString);

        return responseJson;
    }

    /// <summary>
    ///     Creates a chat completion using the Groq API.
    /// </summary>
    /// <param name="request">The request object containing chat completion parameters.</param>
    /// <returns>The API response as a JsonObject.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    public async Task<JsonObject?> CreateChatCompletionAsync(JsonObject request)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ChatCompletionsEndpoint, request);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"API request failed with status code {response.StatusCode}. Response content: {errorContent}");
        }

        return await response.Content.ReadFromJsonAsync<JsonObject>();
    }

    /// <summary>
    ///     Creates a streaming chat completion using the Groq API.
    /// </summary>
    /// <param name="request">The request object containing chat completion parameters.</param>
    /// <returns>An async enumerable of response chunks as JsonObjects.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    public async IAsyncEnumerable<JsonObject?> CreateChatCompletionStreamAsync(JsonObject request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        request["stream"] = true;
        var content = new StringContent(request.ToJsonString(), Encoding.UTF8, "application/json");
        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Post, Endpoints.ChatCompletionsEndpoint) { Content = content };
        using var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (line.StartsWith("data: "))
            {
                var data = line["data: ".Length..];
                if (data != "[DONE]")
                {
                    yield return JsonSerializer.Deserialize<JsonObject>(data);
                }
            }
        }
    }
}
