// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using System.Text.Json.Nodes;
using Groq.Core.Configurations;

namespace Groq.Core.Builders;

/// <summary>
///     Builder class for constructing Groq API chat completion requests.
///     Provides a fluent interface for setting all available parameters.
/// </summary>
public class ChatCompletionRequestBuilder
{
    // Optional core parameters
    private string? _citationOptions;
    private JsonObject? _compoundCustom;
    private bool? _disableToolValidation;
    private JsonArray? _documents;

    // Deprecated parameters (kept for backward compatibility)
    private JsonArray? _excludeDomains;
    private double? _frequencyPenalty;
    private JsonNode? _functionCall;
    private JsonArray? _functions;
    private JsonArray? _includeDomains;
    private bool? _includeReasoning;
    private JsonObject? _logitBias;
    private bool? _logprobs;
    private int? _maxCompletionTokens;

    private int? _maxTokens;

    // Required parameters
    private string _userPrompt = string.Empty;
    private string? _systemPrompt;
    private string? _assistantPrompt;
    private string? _imageUrl;
    private JsonArray? _messages;
    private JsonObject? _metadata;
    private string? _model;
    private int? _n;
    private bool? _parallelToolCalls;
    private double? _presencePenalty;
    private string? _reasoningEffort;
    private string? _reasoningFormat;
    private JsonObject? _responseFormat;
    private JsonObject? _searchSettings;
    private int? _seed;
    private string? _serviceTier;
    private JsonNode? _stop; // Can be string or array
    private bool? _store;
    private bool? _stream;
    private JsonObject? _streamOptions;
    private double? _temperature;
    private JsonNode? _toolChoice; // Can be string or object
    private JsonArray? _tools;
    private int? _topLogprobs;
    private double? _topP;
    private string? _user;

    /// <summary>
    ///     Sets the list of messages comprising the conversation so far.
    /// </summary>
    /// <param name="messages">Array of message objects.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when messages is null.</exception>
    /// <remarks>
    ///     <para>
    ///         <b>IMPORTANT:</b> If this method is used to set messages directly, the following methods will have
    ///         no effect even if called: <see cref="WithUserPrompt"/>, <see cref="WithSystemPrompt"/>,
    ///         <see cref="WithAssistantPrompt"/>, and <see cref="WithImageUrl"/>.
    ///     </para>
    ///     <para>
    ///         Use this method when you need full control over the message structure, or use the convenience
    ///         methods (WithUserPrompt, etc.) to automatically build the messages array.
    ///     </para>
    /// </remarks>
    public ChatCompletionRequestBuilder WithMessages(JsonArray messages)
    {
        ArgumentNullException.ThrowIfNull(messages, nameof(messages));

        _messages = messages;

        return this;
    }

    /// <summary>
    ///     Sets the user prompt for the conversation.
    /// </summary>
    /// <param name="userPrompt">The user's message text.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when userPrompt is null or empty.</exception>
    /// <remarks>
    ///     <para>
    ///         This is a convenience method that automatically builds the messages array. The user prompt is required
    ///         and will be included in the final request as a message with role "user".
    ///     </para>
    ///     <para>
    ///         <b>NOTE:</b> This method has no effect if <see cref="WithMessages"/> was previously called.
    ///         Use either WithMessages for full control, or use this method along with <see cref="WithSystemPrompt"/>,
    ///         <see cref="WithAssistantPrompt"/>, and <see cref="WithImageUrl"/> for automatic message building.
    ///     </para>
    /// </remarks>
    public ChatCompletionRequestBuilder WithUserPrompt(string userPrompt)
    {
        ArgumentException.ThrowIfNullOrEmpty(userPrompt, nameof(userPrompt));

        _userPrompt = userPrompt;

        return this;
    }

    /// <summary>
    ///     Sets the system prompt for the conversation.
    /// </summary>
    /// <param name="systemPrompt">The system message text that sets the context or behavior for the assistant.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when systemPrompt is null or empty.</exception>
    /// <remarks>
    ///     <para>
    ///         This is a convenience method that automatically builds the messages array. The system prompt is optional
    ///         and will be included as the first message with role "system" if provided.
    ///     </para>
    ///     <para>
    ///         <b>NOTE:</b> This method has no effect if <see cref="WithMessages"/> was previously called.
    ///         Use either WithMessages for full control, or use this method along with <see cref="WithUserPrompt"/>
    ///         for automatic message building.
    ///     </para>
    /// </remarks>
    public ChatCompletionRequestBuilder WithSystemPrompt(string systemPrompt)
    {
        ArgumentException.ThrowIfNullOrEmpty(systemPrompt, nameof(systemPrompt));

        _systemPrompt = systemPrompt;

        return this;
    }

    /// <summary>
    ///     Sets the assistant prompt for the conversation.
    /// </summary>
    /// <param name="assistantPrompt">The assistant's message text to provide context for the conversation.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when assistantPrompt is null or empty.</exception>
    /// <remarks>
    ///     <para>
    ///         This is a convenience method that automatically builds the messages array. The assistant prompt is optional
    ///         and will be included as a message with role "assistant" if provided, positioned after the system message
    ///         (if any) and before the user message.
    ///     </para>
    ///     <para>
    ///         <b>NOTE:</b> This method has no effect if <see cref="WithMessages"/> was previously called.
    ///         Use either WithMessages for full control, or use this method along with <see cref="WithUserPrompt"/>
    ///         for automatic message building.
    ///     </para>
    /// </remarks>
    public ChatCompletionRequestBuilder WithAssistantPrompt(string assistantPrompt)
    {
        ArgumentException.ThrowIfNullOrEmpty(assistantPrompt, nameof(assistantPrompt));

        _assistantPrompt = assistantPrompt;

        return this;
    }

    /// <summary>
    ///     Sets the image URL for vision-based requests.
    /// </summary>
    /// <param name="imageUrl">The URL of the image to analyze.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when imageUrl is null or empty.</exception>
    /// <remarks>
    ///     <para>
    ///         This is a convenience method that automatically builds the messages array with multimodal content.
    ///         The image URL is optional and will be included in the user message content alongside the text prompt.
    ///     </para>
    ///     <para>
    ///         <b>NOTE:</b> This method has no effect if <see cref="WithMessages"/> was previously called.
    ///         Use either WithMessages for full control over multimodal content, or use this method along with
    ///         <see cref="WithUserPrompt"/> for automatic message building with vision support.
    ///     </para>
    ///     <para>
    ///         This method is typically used with vision-capable models like Llama 4 Scout or Llama 4 Maverick.
    ///     </para>
    /// </remarks>
    public ChatCompletionRequestBuilder WithImageUrl(string imageUrl)
    {
        ArgumentException.ThrowIfNullOrEmpty(imageUrl, nameof(imageUrl));

        _imageUrl = imageUrl;

        return this;
    }

    private JsonArray BuildMessage()
    {
        if (string.IsNullOrWhiteSpace(_userPrompt))
        {
            throw new InvalidOperationException("User prompt is required. Use WithUserPrompt() to set it.");
        }
        _messages = [];

        if (_systemPrompt is not null)
        {
            _messages.Add(new JsonObject { ["role"] = LlmRoles.SystemRole, ["content"] = _systemPrompt });
        }

        if (_assistantPrompt is not null)
        {
            _messages.Add(new JsonObject { ["role"] = LlmRoles.AssistantRole, ["content"] = _assistantPrompt });
        }

        var userContent = new JsonArray { new JsonObject { ["type"] = "text", ["text"] = _userPrompt } };

        if (_imageUrl is not null)
        {
            userContent.Add(new JsonObject { ["type"] = "image_url", ["image_url"] = new JsonObject { ["url"] = _imageUrl } });
        }

        _messages.Add(new JsonObject { ["role"] = LlmRoles.UserRole, ["content"] = userContent });

        return _messages;
    }

    /// <summary>
    ///     Sets the ID of the model to use.
    /// </summary>
    /// <param name="model">The model ID (e.g., "llama-3.3-70b-versatile").</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithModel(string model)
    {
        _model = model;
        return this;
    }

    /// <summary>
    ///     Sets whether to enable citations in the response.
    /// </summary>
    /// <param name="citationOptions">Either "enabled" or "disabled". Default is "enabled".</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithCitationOptions(string citationOptions)
    {
        _citationOptions = citationOptions;
        return this;
    }

    /// <summary>
    ///     Sets custom configuration of models and tools for Compound.
    /// </summary>
    /// <param name="compoundCustom">Custom compound configuration object.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithCompoundCustom(JsonObject compoundCustom)
    {
        _compoundCustom = compoundCustom;
        return this;
    }

    /// <summary>
    ///     Sets whether to disable tool validation.
    /// </summary>
    /// <param name="disableToolValidation">If true, tools won't be validated. Default is false.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithDisableToolValidation(bool disableToolValidation)
    {
        _disableToolValidation = disableToolValidation;
        return this;
    }

    /// <summary>
    ///     Sets the list of documents to provide context for the conversation.
    /// </summary>
    /// <param name="documents">Array of document objects.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithDocuments(JsonArray documents)
    {
        _documents = documents;
        return this;
    }

    /// <summary>
    ///     Sets the frequency penalty parameter.
    /// </summary>
    /// <param name="frequencyPenalty">Number between -2.0 and 2.0. Default is 0.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithFrequencyPenalty(double frequencyPenalty)
    {
        _frequencyPenalty = frequencyPenalty;
        return this;
    }

    /// <summary>
    ///     Sets whether to include reasoning in the response.
    /// </summary>
    /// <param name="includeReasoning">If true, response will include a reasoning field.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithIncludeReasoning(bool includeReasoning)
    {
        _includeReasoning = includeReasoning;
        return this;
    }

    /// <summary>
    ///     Sets the logit bias to modify likelihood of specified tokens.
    /// </summary>
    /// <param name="logitBias">Object mapping token IDs to bias values.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithLogitBias(JsonObject logitBias)
    {
        _logitBias = logitBias;
        return this;
    }

    /// <summary>
    ///     Sets whether to return log probabilities of output tokens.
    /// </summary>
    /// <param name="logprobs">If true, returns log probabilities. Default is false.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithLogprobs(bool logprobs)
    {
        _logprobs = logprobs;
        return this;
    }

    /// <summary>
    ///     Sets the maximum number of tokens that can be generated.
    /// </summary>
    /// <param name="maxCompletionTokens">Maximum number of completion tokens.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithMaxCompletionTokens(int maxCompletionTokens)
    {
        _maxCompletionTokens = maxCompletionTokens;
        return this;
    }

    /// <summary>
    ///     Sets metadata for the request (not currently supported).
    /// </summary>
    /// <param name="metadata">Metadata object.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithMetadata(JsonObject metadata)
    {
        _metadata = metadata;
        return this;
    }

    /// <summary>
    ///     Sets how many chat completion choices to generate.
    /// </summary>
    /// <param name="n">Number of choices (currently only n=1 is supported). Default is 1.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithN(int n)
    {
        _n = n;
        return this;
    }

    /// <summary>
    ///     Sets whether to enable parallel function calling during tool use.
    /// </summary>
    /// <param name="parallelToolCalls">If true, enables parallel tool calls. Default is true.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithParallelToolCalls(bool parallelToolCalls)
    {
        _parallelToolCalls = parallelToolCalls;
        return this;
    }

    /// <summary>
    ///     Sets the presence penalty parameter.
    /// </summary>
    /// <param name="presencePenalty">Number between -2.0 and 2.0. Default is 0.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithPresencePenalty(double presencePenalty)
    {
        _presencePenalty = presencePenalty;
        return this;
    }

    /// <summary>
    ///     Sets the reasoning effort level.
    /// </summary>
    /// <param name="reasoningEffort">One of: "low", "medium", "high".</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithReasoningEffort(string reasoningEffort)
    {
        _reasoningEffort = reasoningEffort;
        return this;
    }

    /// <summary>
    ///     Sets how to output reasoning tokens.
    /// </summary>
    /// <param name="reasoningFormat">One of: "hidden", "raw", "parsed".</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithReasoningFormat(string reasoningFormat)
    {
        _reasoningFormat = reasoningFormat;
        return this;
    }

    /// <summary>
    ///     Sets the response format specification as a JSON schema.
    /// </summary>
    /// <param name="responseFormat">Response format string (json_schema).</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the responseFormat is null.</exception>
    /// <exception cref="JsonException">Thrown when responseFormat is not valid JSON.</exception>
    public ChatCompletionRequestBuilder WithResponseFormat(string responseFormat, string responseName = "response")
    {
        _responseFormat = new JsonObject
        {
            ["type"] = "json_schema",
            ["json_schema"] = new JsonObject
            {
                ["name"] = responseName,
                ["schema"] = JsonNode.Parse(responseFormat)
            }
        };
        return this;
    }

    /// <summary>
    ///     Sets the search settings for web search functionality.
    /// </summary>
    /// <param name="searchSettings">Search settings object.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithSearchSettings(JsonObject searchSettings)
    {
        _searchSettings = searchSettings;
        return this;
    }

    /// <summary>
    ///     Sets the seed for deterministic sampling.
    /// </summary>
    /// <param name="seed">Random seed integer.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithSeed(int seed)
    {
        _seed = seed;
        return this;
    }

    /// <summary>
    ///     Sets the service tier to use for the request.
    /// </summary>
    /// <param name="serviceTier">One of: "auto", "on_demand", "flex", "performance". Default is "on_demand".</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithServiceTier(string serviceTier)
    {
        _serviceTier = serviceTier;
        return this;
    }

    /// <summary>
    ///     Sets stop sequences where the API will stop generating tokens.
    /// </summary>
    /// <param name="stop">String or array of up to 4 stop sequences.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithStop(JsonNode stop)
    {
        _stop = stop;
        return this;
    }

    /// <summary>
    ///     Sets whether to store the request (not currently supported).
    /// </summary>
    /// <param name="store">Store flag.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithStore(bool store)
    {
        _store = store;
        return this;
    }

    /// <summary>
    ///     Sets whether to stream partial message deltas.
    /// </summary>
    /// <param name="stream">If true, enables streaming. Default is false.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithStream(bool stream)
    {
        _stream = stream;
        return this;
    }

    /// <summary>
    ///     Sets options for streaming response.
    /// </summary>
    /// <param name="streamOptions">Stream options object (only when stream is true).</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithStreamOptions(JsonObject streamOptions)
    {
        _streamOptions = streamOptions;
        return this;
    }

    /// <summary>
    ///     Sets the sampling temperature.
    /// </summary>
    /// <param name="temperature">Number between 0 and 2. Default is 1.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithTemperature(double temperature)
    {
        _temperature = temperature;
        return this;
    }

    /// <summary>
    ///     Sets which tool(s) the model should call.
    /// </summary>
    /// <param name="toolChoice">String ("none", "auto", "required") or object specifying a specific tool.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithToolChoice(JsonNode toolChoice)
    {
        _toolChoice = toolChoice;
        return this;
    }

    /// <summary>
    ///     Sets the list of tools the model may call.
    /// </summary>
    /// <param name="tools">Array of tool objects (max 128 functions).</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithTools(JsonArray tools)
    {
        _tools = tools;
        return this;
    }

    /// <summary>
    ///     Sets the number of most likely tokens to return at each position.
    /// </summary>
    /// <param name="topLogprobs">Integer between 0 and 20.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithTopLogprobs(int topLogprobs)
    {
        _topLogprobs = topLogprobs;
        return this;
    }

    /// <summary>
    ///     Sets the nucleus sampling parameter.
    /// </summary>
    /// <param name="topP">Number between 0 and 1. Default is 1.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithTopP(double topP)
    {
        _topP = topP;
        return this;
    }

    /// <summary>
    ///     Sets a unique identifier for the end-user.
    /// </summary>
    /// <param name="user">User identifier string.</param>
    /// <returns>The ChatCompletionRequestBuilder instance for method chaining.</returns>
    public ChatCompletionRequestBuilder WithUser(string user)
    {
        _user = user;
        return this;
    }

    // Deprecated methods (for backward compatibility)

    /// <summary>
    ///     [Deprecated] Sets domains to exclude from search results.
    ///     Use WithSearchSettings with exclude_domains instead.
    /// </summary>
    [Obsolete("Use WithSearchSettings with exclude_domains instead")]
    public ChatCompletionRequestBuilder WithExcludeDomains(JsonArray excludeDomains)
    {
        _excludeDomains = excludeDomains;
        return this;
    }

    /// <summary>
    ///     [Deprecated] Sets function call behavior.
    ///     Use WithToolChoice instead.
    /// </summary>
    [Obsolete("Use WithToolChoice instead")]
    public ChatCompletionRequestBuilder WithFunctionCall(JsonNode functionCall)
    {
        _functionCall = functionCall;
        return this;
    }

    /// <summary>
    ///     [Deprecated] Sets the list of functions.
    ///     Use WithTools instead.
    /// </summary>
    [Obsolete("Use WithTools instead")]
    public ChatCompletionRequestBuilder WithFunctions(JsonArray functions)
    {
        _functions = functions;
        return this;
    }

    /// <summary>
    ///     [Deprecated] Sets domains to include in search results.
    ///     Use WithSearchSettings with include_domains instead.
    /// </summary>
    [Obsolete("Use WithSearchSettings with include_domains instead")]
    public ChatCompletionRequestBuilder WithIncludeDomains(JsonArray includeDomains)
    {
        _includeDomains = includeDomains;
        return this;
    }

    /// <summary>
    ///     [Deprecated] Sets the maximum number of tokens.
    ///     Use WithMaxCompletionTokens instead.
    /// </summary>
    [Obsolete("Use WithMaxCompletionTokens instead")]
    public ChatCompletionRequestBuilder WithMaxTokens(int maxTokens)
    {
        _maxTokens = maxTokens;
        return this;
    }

    /// <summary>
    ///     Builds and returns the final JsonObject request.
    /// </summary>
    /// <returns>A JsonObject containing all set parameters.</returns>
    /// <exception cref="InvalidOperationException">Thrown when required parameters are missing.</exception>
    public JsonObject Build()
    {
        if (string.IsNullOrEmpty(_model))
        {
            throw new InvalidOperationException("Model is required. Use WithModel() to set it.");
        }

        // Ensure messages are built
        _messages ??= BuildMessage();

        var request = new JsonObject { ["model"] = _model, ["messages"] = _messages };

        // Add optional parameters only if they are set
        if (_responseFormat is not null)
        {
            request["response_format"] = _responseFormat;
        }

        if (_citationOptions is not null)
        {
            request["citation_options"] = _citationOptions;
        }

        if (_compoundCustom is not null)
        {
            request["compound_custom"] = _compoundCustom;
        }

        if (_disableToolValidation.HasValue)

        {
            request["disable_tool_validation"] = _disableToolValidation.Value;
        }

        if (_documents is not null)
        {
            request["documents"] = _documents;
        }

        if (_frequencyPenalty.HasValue)

        {
            request["frequency_penalty"] = _frequencyPenalty.Value;
        }

        if (_includeReasoning.HasValue)

        {
            request["include_reasoning"] = _includeReasoning.Value;
        }

        if (_logitBias is not null)
        {
            request["logit_bias"] = _logitBias;
        }

        if (_logprobs.HasValue)
        {
            request["logprobs"] = _logprobs.Value;
        }

        if (_maxCompletionTokens.HasValue)
        {
            request["max_completion_tokens"] = _maxCompletionTokens.Value;
        }

        if (_metadata is not null)
        {
            request["metadata"] = _metadata;
        }

        if (_n.HasValue)
        {
            request["n"] = _n.Value;
        }

        if (_parallelToolCalls.HasValue)
        {
            request["parallel_tool_calls"] = _parallelToolCalls.Value;
        }

        if (_presencePenalty.HasValue)
        {
            request["presence_penalty"] = _presencePenalty.Value;
        }

        if (_reasoningEffort is not null)
        {
            request["reasoning_effort"] = _reasoningEffort;
        }

        if (_reasoningFormat is not null)
        {
            request["reasoning_format"] = _reasoningFormat;
        }

        if (_searchSettings is not null)
        {
            request["search_settings"] = _searchSettings;
        }

        if (_seed.HasValue)
        {
            request["seed"] = _seed.Value;
        }

        if (_serviceTier is not null)
        {
            request["service_tier"] = _serviceTier;
        }

        if (_stop is not null)
        {
            request["stop"] = _stop;
        }

        if (_store.HasValue)
        {
            request["store"] = _store.Value;
        }

        if (_stream.HasValue)
        {
            request["stream"] = _stream.Value;
        }

        if (_streamOptions is not null)
        {
            request["stream_options"] = _streamOptions;
        }

        if (_temperature.HasValue)
        {
            request["temperature"] = _temperature.Value;
        }

        if (_toolChoice is not null)
        {
            request["tool_choice"] = _toolChoice;
        }

        if (_tools is not null)
        {
            request["tools"] = _tools;
        }

        if (_topLogprobs.HasValue)
        {
            request["top_logprobs"] = _topLogprobs.Value;
        }

        if (_topP.HasValue)
        {
            request["top_p"] = _topP.Value;
        }

        if (_user is not null)
        {
            request["user"] = _user;
        }

        // Deprecated parameters (if still set)
        if (_excludeDomains is not null)
        {
            request["exclude_domains"] = _excludeDomains;
        }

        if (_functionCall is not null)
        {
            request["function_call"] = _functionCall;
        }

        if (_functions is not null)
        {
            request["functions"] = _functions;
        }

        if (_includeDomains is not null)
        {
            request["include_domains"] = _includeDomains;
        }

        if (_maxTokens.HasValue)
        {
            request["max_tokens"] = _maxTokens.Value;
        }

        return request;
    }

    /// <summary>
    ///     Creates a new instance of ChatCompletionRequestBuilder.
    /// </summary>
    /// <returns>A new ChatCompletionRequestBuilder instance.</returns>
    public static ChatCompletionRequestBuilder Builder() => new();
}
