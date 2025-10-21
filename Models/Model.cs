// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace GroqApiLibrary.Models;

public class Model
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("created")]
    public long Created { get; set; }

    [JsonPropertyName("owned_by")]
    public string OwnedBy { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("context_window")]
    public int ContextWindow { get; set; }

    [JsonPropertyName("public_apps")]
    public object? PublicApps { get; set; }

    [JsonPropertyName("max_completion_tokens")]
    public int MaxCompletionTokens { get; set; }
}
