using System.Text.Json.Serialization;

namespace Groq.Core.Models.ChatCompletion;

public class ChatCompletionResponse
{
  [JsonPropertyName("id")]
  public string Id { get; set; } = default!;

  // “object” is a JSON property name; we map it to a C# property called Object.
  [JsonPropertyName("object")]
  public string Object { get; set; } = default!;

  [JsonPropertyName("created")]
  public long Created { get; set; }      // Unix timestamp (seconds)

  [JsonPropertyName("model")]
  public string Model { get; set; } = default!;

  [JsonPropertyName("choices")]
  public List<Choice> Choices { get; set; } = new();

  [JsonPropertyName("usage")]
  public Usage Usage { get; set; } = default!;

  [JsonPropertyName("system_fingerprint")]
  public string? SystemFingerprint { get; set; }

  [JsonPropertyName("x_groq")]
  public XGroq XGroq { get; set; } = default!;
}
