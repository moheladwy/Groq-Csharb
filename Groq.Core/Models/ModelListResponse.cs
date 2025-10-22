// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace Groq.Core.Models;

/// <summary>
/// Represents the response from the Groq API when listing available models.
/// Contains a collection of all models accessible through the API.
/// </summary>
public class ModelListResponse
{
    /// <summary>
    /// Gets or sets the object type identifier for the response.
    /// </summary>
    /// <value>The type of object, typically "list" indicating this is a list response.</value>
    [JsonPropertyName("object")]
    public string Object { get; set; }

    /// <summary>
    /// Gets or sets the list of available models.
    /// </summary>
    /// <value>A collection of <see cref="Model"/> objects representing all available models in the API.</value>
    /// <remarks>
    /// This list includes various model types:
    /// - Chat/text generation models (e.g., Llama, GPT-OSS, Qwen)
    /// - Vision models (e.g., Llama 4 Scout, Llama 4 Maverick)
    /// - Audio models for speech-to-text (e.g., Whisper) and text-to-speech (e.g., PlayAI)
    /// - Content moderation models (e.g., Llama Guard, Llama Prompt Guard)
    /// - Agent/compound systems
    /// </remarks>
    [JsonPropertyName("data")]
    public List<Model> Data { get; set; } = [];
}
