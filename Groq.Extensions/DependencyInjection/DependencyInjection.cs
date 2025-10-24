using System.Net.Http.Headers;
using Groq.Core.Clients;
using Groq.Core.Configurations;
using Groq.Core.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;

namespace Groq.Extensions.DependencyInjection;

/// <summary>
///     Provides extension methods to register Groq API services and related components in the dependency injection
///     container.
/// </summary>
/// <remarks>
///     This static class is designed to simplify the integration of Groq API clients and related dependencies into the
///     application's
///     service collection. It includes a method to register the required services and configure the HTTP client
///     for interacting with the Groq API.
/// </remarks>
public static class DependencyInjection
{
    /// <summary>
    ///     The name of the Groq HTTP client used for making requests to the Groq API.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string GroqHttpClientName = "GroqHttpClient";

    /// <summary>
    ///     Adds Groq API-related services to the dependency injection container of the application.
    /// </summary>
    /// <param name="builder">
    ///     The builder to add the Groq API-related services to.
    /// </param>
    /// <param name="configureOptions">
    ///     An action to configure the <see cref="GroqOptions" /> used for Groq API clients.
    /// </param>
    /// <remarks>
    ///     This method registers various Groq API clients and providers, such as
    ///     <see cref="ChatCompletionClient" />, <see cref="AudioClient" />,
    ///     <see cref="VisionClient" />, <see cref="ToolClient" />, and <see cref="LlmTextProvider" />,
    ///     into the dependency injection container. It also configures the HTTP client factory for creating
    ///     Groq HTTP clients.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///     Thrown when the provided API key is null or empty.
    /// </exception>
    /// <seealso cref="ChatCompletionClient" />
    /// <seealso cref="AudioClient" />
    /// <seealso cref="VisionClient" />
    /// <seealso cref="ToolClient" />
    /// <seealso cref="LlmTextProvider" />
    public static TBuilder AddGroqApiServices<TBuilder>(
        this TBuilder builder,
        Action<GroqOptions> configureOptions)
        where TBuilder : IHostApplicationBuilder
    {
        ArgumentNullException.ThrowIfNull(configureOptions);

        // Register the HttpClient factory for Groq
        builder.Services.Configure(configureOptions);
        builder.Services
            .AddOptions<GroqOptions>()
            .Validate(o => !string.IsNullOrWhiteSpace(o.ApiKey), "Groq:ApiKey is required.")
            .Validate(o => o.MaxRetries >= 0, "Groq:MaxRetries must be >= 0.")
            .Validate(o => o.Timeout > TimeSpan.Zero, "Groq:Timeout must be > 0.")
            .ValidateOnStart();

        builder.AddGroqHttpClientFactory();

        // Register Groq API clients
        builder
            .Services.AddScoped<ChatCompletionClient>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(GroqHttpClientName);
                return new ChatCompletionClient(httpClient);
            })
            .AddScoped<AudioClient>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(GroqHttpClientName);
                return new AudioClient(httpClient);
            })
            .AddScoped<VisionClient>()
            .AddScoped<ToolClient>()
            .AddScoped<LlmTextProvider>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<ChatCompletionClient>();
                var options = sp.GetRequiredService<IOptions<GroqOptions>>().Value;
                return new LlmTextProvider(httpClientFactory, options.Model);
            })
            .AddScoped<GroqClient>(sp =>
            {
                var chatClient = sp.GetRequiredService<ChatCompletionClient>();
                var audioClient = sp.GetRequiredService<AudioClient>();
                var visionClient = sp.GetRequiredService<VisionClient>();
                var toolClient = sp.GetRequiredService<ToolClient>();
                var llmTextProvider = sp.GetRequiredService<LlmTextProvider>();
                return new GroqClient(
                    chatClient,
                    audioClient,
                    visionClient,
                    toolClient,
                    llmTextProvider
                );
            });

        return builder;
    }

    /// <summary>
    ///     Configures and registers the HTTP client factory for creating Groq HTTP clients.
    /// </summary>
    /// <param name="builder">
    ///     The application builder to which the Groq HTTP client factory will be added.
    /// </param>
    /// <returns>
    ///     The modified <see cref="TBuilder" /> instance.
    /// </returns>
    /// <remarks>
    ///     This method configures a named HTTP client factory with the name "GroqHttpClient" that can be used
    ///     to create HTTP clients pre-configured with the Groq API base address, authorization header, and
    ///     standard resilience handlers.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///     Thrown when the provided API key is null or empty.
    /// </exception>
    private static TBuilder AddGroqHttpClientFactory<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services
            .AddHttpClient(GroqHttpClientName)
            .ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<GroqOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.ApiKey);
            })
            .AddStandardResilienceHandler()
            .Configure((options, sp) =>
            {
                var settings = sp.GetRequiredService<IOptions<GroqOptions>>().Value;
                options.Retry = new HttpRetryStrategyOptions
                {
                    Delay = settings.Delay, MaxRetryAttempts = settings.MaxRetries, MaxDelay = settings.MaxDelay
                };
                options.AttemptTimeout = new HttpTimeoutStrategyOptions { Timeout = settings.Timeout };
            });

        return builder;
    }
}
