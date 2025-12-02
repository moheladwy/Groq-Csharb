using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Groq.Core.Configurations;
using Groq.Core.Configurations.Voice;
using Groq.Core.Models;

namespace Groq.Core.Clients;

/// <summary>
///     Represents a client for interacting with the audio-related services of the Groq API.
/// </summary>
/// <remarks>
///     This client provides methods for performing audio-specific operations, such as transcriptions and translations,
///     by leveraging the Groq API. It can be initialized with its own HttpClient or use a shared one for network
///     communication. The class also includes mechanisms for integrating with the AudioClient.
/// </remarks>
public sealed class AudioClient
{
    /// <summary>The HTTP client used for making API requests.</summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    ///     Initializes a new instance of the AudioClient with a provided HttpClient.
    /// </summary>
    /// <remarks>This constructor allows for the use of a shared HttpClient for API requests.</remarks>
    /// <param name="httpClient">
    ///     The <see cref="HttpClient" /> instance to use for audio-related API requests.
    /// </param>
    public AudioClient(HttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    ///     Creates a transcription of an audio file using the Groq API.
    /// </summary>
    /// <param name="audioFile">The audio file stream to transcribe.</param>
    /// <param name="fileName">The name of the audio file.</param>
    /// <param name="model">The model to use for transcription.</param>
    /// <param name="prompt">Optional prompt to guide the transcription.</param>
    /// <param name="responseFormat">The format of the response (default is "json").</param>
    /// <param name="language">Optional language specification for the audio.</param>
    /// <param name="temperature">Optional temperature setting for the transcription.</param>
    /// <returns>The API response as a JsonObject containing the transcription.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    public async Task<JsonObject?> CreateTranscriptionAsync(
        Stream audioFile,
        string fileName,
        string model,
        string? prompt = null,
        string responseFormat = "json",
        string? language = null,
        float? temperature = null)
    {
        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(audioFile);
        content.Add(fileContent, "file", fileName);

        var modelContent = new StringContent(model);
        content.Add(modelContent, "model");

        if (!string.IsNullOrWhiteSpace(prompt))
        {
            var promptContent = new StringContent(prompt);
            content.Add(promptContent, "prompt");
        }

        var responseFormatContent = new StringContent(responseFormat);
        content.Add(responseFormatContent, "response_format");

        if (!string.IsNullOrWhiteSpace(language))
        {
            var languageContent = new StringContent(language);
            content.Add(languageContent, "language");
        }

        if (temperature.HasValue)
        {
            var temperatureContent = new StringContent(temperature.Value.ToString(CultureInfo.CurrentCulture));
            content.Add(temperatureContent, "temperature");
        }

        var response = await _httpClient.PostAsync(Endpoints.TranscriptionsEndpoint, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JsonObject>();
    }

    /// <summary>
    ///     Creates a translation of an audio file to English using the Groq API.
    /// </summary>
    /// <param name="audioFile">The audio file stream to translate.</param>
    /// <param name="fileName">The name of the audio file.</param>
    /// <param name="model">The model to use for translation.</param>
    /// <param name="prompt">Optional prompt to guide the translation.</param>
    /// <param name="responseFormat">The format of the response (default is "json").</param>
    /// <param name="temperature">Optional temperature setting for the translation.</param>
    /// <returns>The API response as a JsonObject containing the translation.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    public async Task<JsonObject?> CreateTranslationAsync(
        Stream audioFile,
        string fileName,
        string model,
        string? prompt = null,
        string responseFormat = "json",
        float? temperature = null)
    {
        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(audioFile);
        content.Add(fileContent, "file", fileName);

        var modelContent = new StringContent(model);
        content.Add(modelContent, "model");

        if (!string.IsNullOrWhiteSpace(prompt))
        {
            var promptContent = new StringContent(prompt);
            content.Add(promptContent, "prompt");
        }

        var responseFormatContent = new StringContent(responseFormat);
        content.Add(responseFormatContent, "response_format");

        if (temperature.HasValue)
        {
            var temperatureContent = new StringContent(temperature.Value.ToString(CultureInfo.CurrentCulture));
            content.Add(temperatureContent, "temperature");
        }

        var response = await _httpClient.PostAsync(Endpoints.TranslationsEndpoint, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JsonObject>();
    }

    /// <summary>
    ///     Creates English speech audio from the provided text input using PlayAI TTS model.
    /// </summary>
    /// <param name="input">The text content to convert to speech. Recommended to keep under 10K characters for best results.</param>
    /// <param name="voice">
    ///     The English voice to use for speech synthesis. See <see cref="EnglishVoices" /> for available
    ///     options.
    /// </param>
    /// <returns>A byte array containing the audio data in WAV format.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    /// <remarks>
    ///     <para>
    ///         This method uses the PlayAI TTS model to generate human-like English speech with customizable voice
    ///         parameters.
    ///     </para>
    ///     <para><b>Model:</b> playai-tts</para>
    ///     <para><b>Pricing:</b> $50.00 per million characters (20,000 characters per $1)</para>
    ///     <para><b>Output Format:</b> WAV audio file</para>
    ///     <para><b>Best Practices:</b> Keep input text under 10K characters for optimal quality and performance.</para>
    ///     <para>
    ///         <b>Use Cases:</b> Creative content generation, voice agents, conversational AI, customer support,
    ///         accessibility tools.
    ///     </para>
    /// </remarks>
    public async Task<byte[]> CreateTextToEnglishSpeechAsync(string input, EnglishVoices voice)
    {
        var requestBody = new JsonObject
        {
            ["input"] = input,
            ["model"] = AudioModels.PLAYAI_TTS.Id,
            ["voice"] = $"{voice}-PlayAI",
            ["response_format"] = "wav"
        };

        var response = await _httpClient.PostAsJsonAsync(Endpoints.TextToSpeechEndpoint, requestBody);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsByteArrayAsync();
        return responseData;
    }

    /// <summary>
    ///     Creates Arabic speech audio from the provided text input using PlayAI TTS Arabic model.
    /// </summary>
    /// <param name="input">The text content to convert to speech. Recommended to keep under 10K characters for best results.</param>
    /// <param name="voice">
    ///     The Arabic voice to use for speech synthesis. See <see cref="ArabicVoices" /> for available
    ///     options.
    /// </param>
    /// <returns>A byte array containing the audio data in WAV format.</returns>
    /// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
    /// <remarks>
    ///     <para>
    ///         This method uses the PlayAI TTS Arabic model to generate human-like Arabic speech with customizable voice
    ///         parameters.
    ///     </para>
    ///     <para><b>Model:</b> playai-tts-arabic</para>
    ///     <para><b>Languages:</b> Arabic (specialized) and English</para>
    ///     <para><b>Pricing:</b> $50.00 per million characters (20,000 characters per $1)</para>
    ///     <para><b>Output Format:</b> WAV audio file</para>
    ///     <para>
    ///         <b>Best Practices:</b> Keep input text under 10K characters; consider cultural sensitivity for Arabic
    ///         contexts.
    ///     </para>
    ///     <para>
    ///         <b>Use Cases:</b> Arabic creative content generation, voice agents, conversational AI, customer support for
    ///         Arabic speakers, accessibility tools.
    ///     </para>
    /// </remarks>
    public async Task<byte[]> CreateTextToArabicSpeechAsync(string input, ArabicVoices voice)
    {
        var requestBody = new JsonObject
        {
            ["input"] = input,
            ["model"] = AudioModels.PLAYAI_TTS_ARABIC.Id,
            ["voice"] = $"{voice}-PlayAI",
            ["response_format"] = "wav"
        };

        var response = await _httpClient.PostAsJsonAsync(Endpoints.TextToSpeechEndpoint, requestBody);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsByteArrayAsync();
        return responseData;
    }
}
