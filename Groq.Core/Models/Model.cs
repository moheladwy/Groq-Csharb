// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace Groq.Core.Models;

/// <summary>
///     Represents a language model available in the Groq API.
///     Contains metadata about the model's capabilities, limitations, and configuration.
/// </summary>
public class Model
{
    /// <summary>
    ///     Gets or sets the unique identifier for the model.
    /// </summary>
    /// <value>
    ///     The model ID used to specify which model to use in API requests (e.g., "llama-3.1-8b-instant",
    ///     "whisper-large-v3").
    /// </value>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     Gets or sets the object type identifier.
    /// </summary>
    /// <value>The type of object, typically "model".</value>
    [JsonPropertyName("object")]
    public required string Object { get; set; }

    /// <summary>
    ///     Gets or sets the Unix timestamp (in seconds) when the model was created.
    /// </summary>
    /// <value>A Unix timestamp representing the model's creation date.</value>
    [JsonPropertyName("created")]
    public long Created { get; set; }

    /// <summary>
    ///     Gets or sets the organization that owns and maintains the model.
    /// </summary>
    /// <value>The owner organization name (e.g., "Meta", "OpenAI", "Groq", "Alibaba Cloud", "PlayAI", "Moonshot AI").</value>
    [JsonPropertyName("owned_by")]
    public required string OwnedBy { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the model is currently active and available for use.
    /// </summary>
    /// <value><c>true</c> if the model is active and can be used; otherwise, <c>false</c>.</value>
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    /// <summary>
    ///     Gets or sets the maximum number of tokens the model can process in its context window.
    /// </summary>
    /// <value>The context window size in tokens, determining how much text the model can consider at once.</value>
    /// <remarks>Larger context windows allow for processing longer documents or maintaining longer conversation histories.</remarks>
    [JsonPropertyName("context_window")]
    public int ContextWindow { get; set; }

    /// <summary>
    ///     Gets or sets information about public applications using this model.
    /// </summary>
    /// <value>An object containing public app information, or <c>null</c> if not applicable.</value>
    [JsonPropertyName("public_apps")]
    public object? PublicApps { get; set; }

    /// <summary>
    ///     Gets or sets the maximum number of tokens the model can generate in a single completion.
    /// </summary>
    /// <value>The maximum output token limit for model completions.</value>
    /// <remarks>This limit determines the maximum length of generated responses.</remarks>
    [JsonPropertyName("max_completion_tokens")]
    public int MaxCompletionTokens { get; set; }
}
