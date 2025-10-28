// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace Groq.Core.Models.ChatCompletion;

public class XGroq
{
    /// <summary>
    ///     Gets or sets the unique identifier for the chat completion response.
    /// </summary>
    /// <value>The unique identifier for the chat completion response.</value>
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}
