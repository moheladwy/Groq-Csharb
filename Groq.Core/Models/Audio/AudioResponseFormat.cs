using System;
using System.Text.Json.Serialization;

namespace Groq.Core.Models.Audio;

/// <summary>
/// Formats that can be returned by the transcription / translation endpoints.
/// </summary>
public enum AudioResponseFormat
{
  [JsonPropertyName("json")] Json,
  [JsonPropertyName("text")] Text,
  [JsonPropertyName("verbose_json")] VerboseJson
}