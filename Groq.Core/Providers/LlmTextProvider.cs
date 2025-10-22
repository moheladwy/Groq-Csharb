using System.Text.Json;
using System.Text.Json.Nodes;
using Groq.Core.Clients;
using Groq.Core.Interfaces;
using Groq.Core.Models;
using Groq.Core.Settings;

namespace Groq.Core.Providers;

/// <summary>
///     Provides integration with Groq LLM (Large Language Model) API for text generation.
///     Implements <see cref="ILlmTextProvider" /> interface for consistent LLM operations.
/// </summary>
public sealed class LlmTextProvider : ILlmTextProvider
{
    private readonly ChatCompletionClient _client;
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
    ///     Generates text based on the provided user prompt using the configured LLM model.
    /// </summary>
    /// <param name="userPrompt">The user's input prompt for text generation.</param>
    /// <returns>The generated text response from the model.</returns>
    /// <param name="structureOutputJsonFormat">A JSON format string that defines the desired structure of the output.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="userPrompt"/> is null.</exception>
    public async Task<string> GenerateAsync(
        string userPrompt,
        string? structureOutputJsonFormat = null
    )
    {
        ArgumentNullException.ThrowIfNull(userPrompt);
        var request = new JsonObject
        {
            ["model"] = _model,
            ["messages"] = JsonSerializer.SerializeToNode(
                new[] { new { role = LlmRoles.UserRole, content = userPrompt } }
            ),
        };

        if (structureOutputJsonFormat is not null)
        {
            request.Add("response_format", new JsonObject
            {
                ["type"] = "json_schema",
                ["json_schema"] = structureOutputJsonFormat,
            });
        }

        var response = await _client.CreateChatCompletionAsync(request);
        return response?["choices"]?[0]?["message"]?["content"]?.GetValue<string>() ?? string.Empty;
    }

    /// <summary>
    ///     Generates a response using the LLM based on both system and user prompts with a structured output format.
    /// </summary>
    /// <param name="systemPrompt">The system prompt providing context or instructions to the LLM.</param>
    /// <param name="userPrompt">The user's input prompt for text generation.</param>
    /// <param name="structureOutputJsonFormat">A JSON format string that defines the desired structure of the output.</param>
    /// <returns> A task that represents the asynchronous operation, containing the generated text response.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="systemPrompt" />, or <paramref name="userPrompt" /> is null.</exception>
    public async Task<string> GenerateAsync(
        string systemPrompt,
        string userPrompt,
        string? structureOutputJsonFormat = null
    )
    {
        ArgumentNullException.ThrowIfNull(systemPrompt);
        ArgumentNullException.ThrowIfNull(userPrompt);

        var request = new JsonObject
        {
            ["model"] = _model,
            ["messages"] = JsonSerializer.SerializeToNode(
                new[]
                {
                    new { role = LlmRoles.SystemRole, content = systemPrompt },
                    new { role = LlmRoles.UserRole, content = userPrompt },
                }
            )
        };

        if (structureOutputJsonFormat is not null)
        {
            request.Add("response_format", new JsonObject
            {
                ["type"] = "json_schema",
                ["json_schema"] = structureOutputJsonFormat,
            });
        }

        var response = await _client.CreateChatCompletionAsync(request);
        var result =
            response?["choices"]?[0]?["message"]?["content"]?.GetValue<string>() ?? string.Empty;

        return result;
    }
}
