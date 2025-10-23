using System.Net.Http.Headers;
using Groq.Core.Clients;
using Groq.Core.Providers;
using Groq.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Resilience;

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
    public static readonly string GroqHttpClientName = "GroqHttpClient";

    /// <summary>
    ///     Adds Groq API-related services to the dependency injection container of the application.
    /// </summary>
    /// <param name="builder">
    ///     The builder to add the Groq API-related services to.
    /// </param>
    /// <param name="apiKey">
    ///     The API key used for authorization with the Groq API.
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
    public static TBuilder AddGroqApiServices<TBuilder>(this TBuilder builder, string apiKey)
        where TBuilder : IHostApplicationBuilder
    {
        ArgumentException.ThrowIfNullOrEmpty(apiKey);

        // Register the HttpClient factory for Groq
        builder.AddGroqHttpClientFactory(apiKey);

        // Register Groq API clients
        builder.Services
            .AddScoped<ChatCompletionClient>(sp =>
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
            .AddScoped<LlmTextProvider>()
            ;

        return builder;
    }


    /// <summary>
    ///     Configures and registers the HTTP client factory for creating Groq HTTP clients.
    /// </summary>
    /// <param name="builder">
    ///     The application builder to which the Groq HTTP client factory will be added.
    /// </param>
    /// <param name="apiKey">
    ///     The API key used for authorization with the Groq API.
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
    private static TBuilder AddGroqHttpClientFactory<TBuilder>(this TBuilder builder, string apiKey)
        where TBuilder : IHostApplicationBuilder
    {
        ArgumentException.ThrowIfNullOrEmpty(apiKey);

        builder.Services.AddHttpClient(GroqHttpClientName, client =>
        {
            client.BaseAddress = new Uri(Endpoints.BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }).AddStandardResilienceHandler();

        return builder;
    }
}
