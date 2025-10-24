namespace Groq.Core.Configurations;

/// <summary>
///     Configuration settings for the Groq API client.
/// </summary>
public class GroqOptions
{
    /// <summary>
    ///     Gets or sets the API key for authenticating with the Groq API.
    ///     This property is required and must be provided.
    /// </summary>
    public required string ApiKey { get; set; }

    /// <summary>
    ///     Gets or sets the base URL for the Groq API endpoint.
    ///     Defaults https://api.groq.com/openai/v1/
    /// </summary>
    public string BaseUrl { get; set; } = Endpoints.BaseUrl;

    /// <summary>
    ///     Gets or sets the model identifier to use for API requests.
    ///     This property is optional and can be null.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    ///     Gets or sets the timeout duration for API requests.
    ///     Defaults to 100 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    ///     Gets or sets the maximum number of retry attempts for failed API requests.
    ///     Defaults to 3 retries.
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    ///     Gets or sets the initial delay duration between retry attempts.
    ///     Defaults to 2 seconds.
    /// </summary>
    public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(2);

    /// <summary>
    ///     Gets or sets the maximum delay duration between retry attempts.
    ///     Defaults to 20 seconds.
    /// </summary>
    public TimeSpan MaxDelay { get; set; } = TimeSpan.FromSeconds(20);
}
