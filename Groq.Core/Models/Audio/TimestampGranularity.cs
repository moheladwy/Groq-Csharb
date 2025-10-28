using System;
using System.Text.Json.Serialization;

namespace Groq.Core.Models.Audio;

/// <summary>
/// Granularities for timestamps in a verbose‑json transcription.
/// </summary>
public enum TimestampGranularity
{
  [JsonPropertyName("word")]
  Word,
  [JsonPropertyName("segment")]
  Segment
}
