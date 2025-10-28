using System.Text.Json.Serialization;

namespace Groq.Core.Models.ChatCompletion;

/// <summary>
/// The assistant (or user) message returned in a choice.
/// </summary>
public class Message
{
  [JsonPropertyName("role")]
  public string Role { get; set; } = default!;   // e.g. "assistant"

  [JsonPropertyName("content")]
  public string Content { get; set; } = default!;
}
