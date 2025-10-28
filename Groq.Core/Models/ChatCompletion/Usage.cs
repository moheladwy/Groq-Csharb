// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace Groq.Core.Models.ChatCompletion;

/// <summary>
/// Token‑usage statistics for the request/response.
/// </summary>
public class Usage
{
    /// <summary>
    ///     Gets or sets the queue time for the request.
    /// </summary>
    /// <value>The queue time for the request.</value>
    [JsonPropertyName("queue_time")]
    public double QueueTime { get; set; }

    /// <summary>
    ///     Gets or sets the prompt tokens for the request.
    /// </summary>
    /// <value>The prompt tokens for the request.</value>
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }

    /// <summary>
    ///     Gets or sets the prompt time for the request.
    /// </summary>
    /// <value>The prompt time for the request.</value>
    [JsonPropertyName("prompt_time")]
    public double PromptTime { get; set; }

    /// <summary>
    ///     Gets or sets the completion tokens for the request.
    /// </summary>
    /// <value>The completion tokens for the request.</value>
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }

    /// <summary>
    ///     Gets or sets the completion time for the request.
    /// </summary>
    /// <value>The completion time for the request.</value>
    [JsonPropertyName("completion_time")]
    public double CompletionTime { get; set; }

    /// <summary>
    ///     Gets or sets the total tokens for the request.
    /// </summary>
    /// <value>The total tokens for the request.</value>
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }

    /// <summary>
    ///     Gets or sets the total time for the request.
    /// </summary>
    /// <value>The total time for the request.</value>
    [JsonPropertyName("total_time")]
    public double TotalTime { get; set; }
}
