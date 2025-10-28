using System.Text.Json;
using System.Text.Json.Serialization;

namespace Groq.Core.Models.Audio;

public class AudioSpeechRequest
{
  // ── Required ──────────────────────────────────────────────────────
  [JsonPropertyName("input")]
  public string Input { get; set; } = default!;               // text to be spoken【5†L636-L639】

  [JsonPropertyName("model")]
  public string Model { get; set; } = default!;               // TTS model ID (e.g. "playai-tts")【5†L640-L642】

  [JsonPropertyName("voice")]
  public string Voice { get; set; } = default!;               // voice name (e.g. "Fritz-PlayAI")【5†L644-L647】

  // ── Optional ──────────────────────────────────────────────────────
  [JsonPropertyName("response_format")]
  public SpeechResponseFormat? ResponseFormat { get; set; }   // default = mp3【5†L649-L654】

  [JsonPropertyName("sample_rate")]
  public int? SampleRate { get; set; }                        // default = 48000【5†L656-L660】

  [JsonPropertyName("speed")]
  public double? Speed { get; set; }                          // 0.5‑5, default = 1【5†L662-L664】

  // ── Serialisation helper (JSON payload) ───────────────────────────
  public string ToJson()
  {
    return JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
      PropertyNamingPolicy = null
    });
  }
}