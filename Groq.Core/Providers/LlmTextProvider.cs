using Groq.Core.Builders;
using Groq.Core.Clients;
using Groq.Core.Models;

namespace Groq.Core.Providers;

/// <summary>
///     Provides integration with Groq LLM (Large Language Model) API for text generation.
/// </summary>
public sealed class LlmTextProvider
{
    /// <summary>
    ///     Represents an instance of <see cref="ChatCompletionClient" /> utilized for creating chat completions
    ///     in the LLM (Large Language Model) operations.
    ///     This client enables communication with the underlying API to generate and retrieve
    ///     language responses based on provided prompts.
    /// </summary>
    private readonly ChatCompletionClient _client;

    /// <summary>
    ///     Specifies the identifier of the LLM (Large Language Model) to be utilized for generating text responses.
    ///     This value determines which model will be used when interacting with the API via the
    ///     <see cref="ChatCompletionClient" /> for text generation tasks.
    /// </summary>
    private readonly string _model;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LlmTextProvider" /> class with a custom HTTP client.
    /// </summary>
    /// <param name="chatCompletionClient">
    ///     The <see cref="ChatCompletionClient" /> instance to be used for API requests.
    /// </param>
    /// <param name="model">
    ///     The model Id to use for text generation.
    ///     If null, defaults to <see cref="ChatModels.OPENAI_GPT_OSS_120B" />.
    /// </param>
    public LlmTextProvider(ChatCompletionClient chatCompletionClient, string? model = null)
    {
        _client = chatCompletionClient;
        _model = model ?? ChatModels.OPENAI_GPT_OSS_120B.Id;
    }

    /// <summary>
    ///     Generates a response using the LLM based on both system and user prompts with a structured output format.
    /// </summary>
    /// <param name="systemPrompt">The system prompt providing context or instructions to the LLM.</param>
    /// <param name="userPrompt">The user's input prompt for text generation.</param>
    /// <param name="structureOutputJsonFormat">A JSON format string that defines the desired structure of the output.</param>
    /// <param name="model">The model Id to use for text generation. If null, defaults to the provider's model.</param>
    /// <returns> A task that represents the asynchronous operation, containing the generated text response.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when the <paramref name="userPrompt" /> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Thrown when the <paramref name="structureOutputJsonFormat" /> is not a valid JSON string.
    /// </exception>
    public async Task<string> GenerateAsync(
        string userPrompt,
        string? systemPrompt = null,
        string? structureOutputJsonFormat = null,
        string? model = null
    )
    {
        ArgumentNullException.ThrowIfNull(userPrompt);

        var builder = ChatCompletionRequestBuilder
            .Create()
            .WithModel(model ?? _model)
            .WithMessages(userPrompt, systemPrompt);

        if (structureOutputJsonFormat is not null)
        {
            builder = builder.WithResponseFormat(structureOutputJsonFormat);
        }

        var request = builder.Build();
        var response = await _client.CreateChatCompletionAsync(request);
        var result = response?["choices"]?[0]?["message"]?["content"]?.GetValue<string>() ?? string.Empty;

        return result;
    }

    /// <summary>
    ///     Generates a streaming response using the LLM based on both system and
    ///     user prompts with a structured output format.
    /// </summary>
    /// <param name="userPrompt">The user's input prompt for text generation.</param>
    /// <param name="systemPrompt">The system prompt providing context or instructions to the LLM.</param>
    /// <param name="structureOutputJsonFormat">A JSON format string that defines the desired structure of the output.</param>
    /// <param name="model">The model Id to use for text generation. If null, defaults to the provider's model.</param>
    /// <returns> An asynchronous stream of generated text chunks.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when the <paramref name="userPrompt" /> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Thrown when the <paramref name="structureOutputJsonFormat" /> is not a valid JSON string.
    /// </exception>
    public async IAsyncEnumerable<string> GenerateStreamingAsync(
        string userPrompt,
        string? systemPrompt = null,
        string? structureOutputJsonFormat = null,
        string? model = null
    )
    {
        ArgumentNullException.ThrowIfNull(userPrompt);

        var builder = ChatCompletionRequestBuilder
            .Create()
            .WithModel(model ?? _model)
            .WithMessages(userPrompt, systemPrompt);

        if (structureOutputJsonFormat is not null)
        {
            builder = builder.WithResponseFormat(structureOutputJsonFormat);
        }

        var request = builder.Build();

        await foreach (var chunk in _client.CreateChatCompletionStreamAsync(request))
        {
            yield return chunk?["choices"]?[0]?["delta"]?["content"]?.ToString() ?? string.Empty;
        }
    }
}
