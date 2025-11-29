using System.Text.Json;
using System.Text.Json.Nodes;
using Groq.Core.Builders;
using Groq.Core.Configurations;
using Groq.Core.Models;

namespace Groq.Core.Clients;

/// <summary>
///     A client for interacting with the Groq API, including functionalities such as listing models
///     and running conversations enhanced with tool integrations.
/// </summary>
/// <remarks>
///     This class provides methods to communicate with Groq API endpoints and handle
///     authentication via an API key. It supports operations such as retrieving a list of models
///     and executing conversations with tool assistance.
/// </remarks>
public sealed class ToolClient
{
    /// <summary>Handles API communication for generating chat completions using the Groq API.</summary>
    private readonly ChatCompletionClient _chatCompletionClient;

    /// <summary>
    ///     Initializes a new instance of the ToolClient with a specified ChatCompletionClient and HttpClient.
    /// </summary>
    /// <param name="chatCompletionClient">
    ///     The client of type <see cref="ChatCompletionClient" /> responsible for handling chat completions
    ///     with the Groq API.
    /// </param>
    public ToolClient(ChatCompletionClient chatCompletionClient) => _chatCompletionClient = chatCompletionClient;

    /// <summary>
    ///     Runs a multi-turn conversation with tool-augmented capabilities using the Groq API.
    /// </summary>
    /// <param name="userPrompt">The initial user prompt to start the conversation.</param>
    /// <param name="tools">Collection of tools that the model can use during the conversation.</param>
    /// <param name="model">The model to use for the conversation.</param>
    /// <param name="systemMessage">The system message providing context and instructions for the model.</param>
    /// <returns>The final AI response as a string after tool interactions are complete.</returns>
    /// <exception cref="HttpRequestException">Thrown when API requests fail.</exception>
    /// <exception cref="JsonException">Thrown when parsing JSON responses fails.</exception>
    /// <exception cref="Exception">Thrown for any other unexpected errors.</exception>
    public async Task<string> RunConversationWithToolsAsync(
        string userPrompt,
        IReadOnlyCollection<Tool> tools,
        string model,
        string systemMessage
    )
    {
        try
        {
            var toolsJsonArray = new JsonArray();
            tools.ToList().ForEach(t => toolsJsonArray.Add(new JsonObject
            {
                ["type"] = t.Type,
                ["function"] = new JsonObject
                {
                    ["name"] = t.Function.Name,
                    ["description"] = t.Function.Description,
                    ["parameters"] = t.Function.Parameters
                }
            }));

            var request = ChatCompletionRequestBuilder
                .Builder()
                .WithModel(model)
                .WithUserPrompt(userPrompt)
                .WithSystemPrompt(systemMessage)
                .WithTools(toolsJsonArray)
                .WithToolChoice("auto")
                .Build();

            var response = await _chatCompletionClient.CreateChatCompletionAsync(request);
            var responseMessage = response?["choices"]?[0]?["message"]?.AsObject();
            var toolCalls = responseMessage?["tool_calls"]?.AsArray();

            if (toolCalls is null || toolCalls.Count == 0)
            {
                return responseMessage?["content"]?.GetValue<string>() ?? string.Empty;
            }

            // Build messages list independently from the request object
            var initialMessages = request["messages"]!.AsArray();
            var messages = initialMessages.Select(m => m).ToList(); // Copy initial messages
            foreach (var toolCall in toolCalls)
            {
                var functionName = toolCall?["function"]?["name"]?.GetValue<string>();
                var functionArgs = toolCall?["function"]?["arguments"]?.GetValue<string>();
                var toolCallId = toolCall?["id"]?.GetValue<string>();

                if (string.IsNullOrEmpty(functionName) || string.IsNullOrEmpty(functionArgs))
                {
                    continue;
                }

                var tool = tools.ToList().Find(t => t.Function.Name == functionName);
                if (tool is null)
                {
                    continue;
                }

                var functionResponse = await tool.Function.ExecuteAsync(functionArgs);
                messages.Add(new JsonObject
                    {
                        ["tool_call_id"] = toolCallId,
                        ["role"] = LlmRoles.ToolRole,
                        ["name"] = functionName,
                        ["content"] = functionResponse
                    }
                );
            }

            request["messages"] = JsonSerializer.SerializeToNode(messages);
            var secondResponse = await _chatCompletionClient.CreateChatCompletionAsync(request);
            return secondResponse?["choices"]?[0]?["message"]?["content"]?.GetValue<string>() ?? string.Empty;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }
}
