using System;
using System.Text.Json.Serialization;

namespace Groq.Core.Models.Audio;

/// <summary>
/// Audio file formats that can be requested from the TTS endpoint.
/// </summary>
public enum SpeechResponseFormat
{
  [JsonPropertyName("flac")] Flac,
  [JsonPropertyName("mp3")] Mp3,
  [JsonPropertyName("mulaw")] Mulaw,
  [JsonPropertyName("ogg")] Ogg,
  [JsonPropertyName("wav")] Wav
}
