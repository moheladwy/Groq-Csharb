using System.Text.Json.Serialization;

namespace Groq.Core.Models.Audio;

public class AudioTranslationRequest
{
  // ── Required ──────────────────────────────────────────────────────
  [JsonPropertyName("model")]
  public string Model { get; set; } = default!;               // model ID (e.g. "whisper-large-v3")【5†L574-L577】

  // ── Optional (multipart fields) ──────────────────────────────────
  [JsonIgnore]
  public Stream? File { get; set; }

  [JsonPropertyName("prompt")]
  public string? Prompt { get; set; }                         // guidance text (English)【5†L584-L587】

  [JsonPropertyName("response_format")]
  public AudioResponseFormat? ResponseFormat { get; set; }    // json | text | verbose_json【5†L589-L594】

  [JsonPropertyName("temperature")]
  public double? Temperature { get; set; }                    // 0‑1, default 0【5†L596-L600】

  [JsonPropertyName("url")]
  public string? Url { get; set; }                            // URL of audio (Base64‑URL supported)【5†L604-L608】

  // ── Helper to build multipart/form-data content ─────────────────────
  public MultipartFormDataContent ToMultipartContent(string? fileName = null)
  {
    var content = new MultipartFormDataContent
    {
        { new StringContent(Model), "model" }
    };

    if (!string.IsNullOrWhiteSpace(Prompt))
      content.Add(new StringContent(Prompt), "prompt");
    if (ResponseFormat.HasValue)
      content.Add(new StringContent(ResponseFormat.Value.ToString().ToLower()), "response_format");
    if (Temperature.HasValue)
      content.Add(new StringContent(Temperature.Value.ToString("0.##")), "temperature");
    if (!string.IsNullOrWhiteSpace(Url))
      content.Add(new StringContent(Url), "url");

    if (File != null)
    {
      var fileContent = new StreamContent(File);
      var name = fileName ?? "audio";
      fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
      content.Add(fileContent, "file", name);
    }

    return content;
  }
}