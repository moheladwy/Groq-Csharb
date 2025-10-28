using System;
using System.Text.Json.Serialization;

namespace Groq.Core.Models.Audio;

public class AudioTranscriptionRequest
{
  // ── Required ──────────────────────────────────────────────────────
  [JsonPropertyName("model")]
  public string Model { get; set; } = default!;               // model ID (e.g. "whisper-large-v3")【5†L497-L500】

  // ── Optional (multipart fields) ──────────────────────────────────
  /// <summary>
  /// The binary audio file (flac, mp3, mp4, …). Either this or <see cref="Url"/> must be set.
  /// </summary>
  [JsonIgnore]                                              // not part of JSON payload
  public Stream? File { get; set; }

  /// <summary>
  /// Optional ISO‑639‑1 language code (e.g. "en"). Improves accuracy【5†L509-L512】.
  /// </summary>
  [JsonPropertyName("language")]
  public string? Language { get; set; }

  /// <summary>
  /// Optional prompt that guides the model; should be in the same language【5†L514-L517】.
  /// </summary>
  [JsonPropertyName("prompt")]
  public string? Prompt { get; set; }

  /// <summary>
  /// Desired output format. Default = json【5†L519-L525】.
  /// </summary>
  [JsonPropertyName("response_format")]
  public AudioResponseFormat? ResponseFormat { get; set; }

  /// <summary>
  /// Sampling temperature (0‑1). Default = 0【5†L526-L530】.
  /// </summary>
  [JsonPropertyName("temperature")]
  public double? Temperature { get; set; }

  /// <summary>
  /// Timestamp granularities (only when <c>response_format=verbose_json</c>)【5†L534-L540】.
  /// </summary>
  [JsonPropertyName("timestamp_granularities")]
  public List<TimestampGranularity>? TimestampGranularities { get; set; }

  /// <summary>
  /// A URL that points to the audio file (Base64‑URL supported). Either this or <see cref="File"/> must be set【5†L542-L545】.
  /// </summary>
  [JsonPropertyName("url")]
  public string? Url { get; set; }

  // ── Helper to build multipart/form-data content ─────────────────────
  public MultipartFormDataContent ToMultipartContent(string? fileName = null)
  {
    var content = new MultipartFormDataContent();

    // Required fields
    content.Add(new StringContent(Model), "model");

    // Optional scalar fields
    if (!string.IsNullOrWhiteSpace(Language)) content.Add(new StringContent(Language), "language");
    if (!string.IsNullOrWhiteSpace(Prompt)) content.Add(new StringContent(Prompt), "prompt");
    if (ResponseFormat.HasValue) content.Add(new StringContent(ResponseFormat.Value.ToString().ToLower()), "response_format");
    if (Temperature.HasValue) content.Add(new StringContent(Temperature.Value.ToString("0.##")), "temperature");
    if (!string.IsNullOrWhiteSpace(Url)) content.Add(new StringContent(Url), "url");

    // Timestamp granularities (comma‑separated)
    if (TimestampGranularities != null && TimestampGranularities.Count > 0)
    {
      var joined = string.Join(",", TimestampGranularities);
      content.Add(new StringContent(joined), "timestamp_granularities");
    }

    // File (binary) part
    if (File != null)
    {
      var fileContent = new StreamContent(File);
      // Use the provided file name or a generic one
      var name = fileName ?? "audio";
      fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
      content.Add(fileContent, "file", name);
    }

    return content;
  }
}