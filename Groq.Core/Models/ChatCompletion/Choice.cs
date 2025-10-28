using System.Text.Json;
using System.Text.Json.Serialization;

namespace Groq.Core.Models.ChatCompletion;

/// <summary>
/// A single completion choice.
/// </summary>
public class Choice
{
  [JsonPropertyName("index")]
  public int Index { get; set; }

  [JsonPropertyName("message")]
  public Message Message { get; set; } = default!;

  // The API currently returns null here, but when log‑probs are enabled it will be an object.
  // Using JsonElement lets us deserialize the raw JSON without defining a detailed schema.
  [JsonPropertyName("logprobs")]
  public JsonElement? LogProbs { get; set; }

  [JsonPropertyName("finish_reason")]
  public string? FinishReason { get; set; }
}
