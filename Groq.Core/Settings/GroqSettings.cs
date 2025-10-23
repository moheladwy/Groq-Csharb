using System;

namespace Groq.Core.Settings;

public class GroqSettings
{
  public required string ApiKey { get; set; }
  public string BaseUrl { get; set; } = string.Empty;
  public string? Model { get; set; }
  public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);
  public int MaxRetries { get; set; } = 3;
  public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(2);
  public TimeSpan MaxDelay { get; set; } = TimeSpan.FromSeconds(20);
}
