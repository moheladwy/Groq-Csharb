using System.Text.Json;
using System.Text.Json.Nodes;
using GroqApiLibrary.Clients;
using GroqApiLibrary.Interfaces;
using GroqApiLibrary.Models;

namespace GroqApiLibrary.Providers;

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
  /// </param>
  public LlmTextProvider(
    ChatCompletionClient chatCompletionClient,
    string? model = ChatModels.OPENAI_GPT_OSS_120B
    )
  {
    _client = chatCompletionClient;
    _model = model ?? ChatModels.OPENAI_GPT_OSS_120B;
  }

  /// <summary>
  ///     Generates text based on the provided user prompt using the configured LLM model.
  /// </summary>
  /// <param name="userPrompt">The user's input prompt for text generation.</param>
  /// <returns>The generated text response from the model.</returns>
  public async Task<string> GenerateAsync(string userPrompt)
  {
    var request = new JsonObject
    {
      ["model"] = _model,
      ["messages"] = JsonSerializer.SerializeToNode(new[]
        {
                new { role = LlmRoles.UserRole, content = userPrompt }
        })
    };

    var response = await _client.CreateChatCompletionAsync(request);
    return response?["choices"]?[0]?["message"]?["content"]?.GetValue<string>() ?? string.Empty;
  }

  /// <summary>
  ///     Generates text based on both system and user prompts using the configured LLM model.
  /// </summary>
  /// <param name="systemPrompt">The system instructions or context for the generation.</param>
  /// <param name="userPrompt">The user's input prompt for text generation.</param>
  /// <returns>The generated text response from the model.</returns>
  public async Task<string> GenerateAsync(string systemPrompt, string userPrompt)
  {
    var request = new JsonObject
    {
      ["model"] = _model,
      ["messages"] = JsonSerializer.SerializeToNode(new[]
        {
                new { role = LlmRoles.SystemRole, content = systemPrompt },
                new { role = LlmRoles.UserRole, content = userPrompt }
            })
    };

    var response = await _client.CreateChatCompletionAsync(request);
    var result = response?["choices"]?[0]?["message"]?["content"]?.GetValue<string>() ?? string.Empty;

    return result;
  }
}

