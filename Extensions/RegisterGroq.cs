using System.Net.Http.Headers;
using GroqApiLibrary.Clients;
using GroqApiLibrary.Providers;
using GroqApiLibrary.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GroqApiLibrary.Extensions;

/// <summary>
/// Provides extension methods to register Groq API services and related components in the dependency injection container.
/// </summary>
/// <remarks>
/// This static class is designed to simplify the integration of Groq API clients and related dependencies into the application's
/// service collection. It includes a method to register the required services and configure the HTTP client
/// for interacting with the Groq API.
/// </remarks>
public static class RegisterGroq
{
    /// <summary>
    ///     Adds Groq API-related services to the dependency injection container of the application.
    /// </summary>
    /// <param name="builder">
    ///     The builder to add the Groq API-related services to.
    /// </param>
    /// <param name="apiKey">
    ///    The API key used for authorization with the Groq API.
    /// </param>
    /// <remarks>
    ///     This method registers various Groq API clients and providers, such as
    ///     <see cref="ChatCompletionClient" />, <see cref="AudioClient" />,
    ///     <see cref="VisionClient" />, <see cref="ToolClient" />, and <see cref="LlmTextProvider" />,
    ///     into the dependency injection container. It also configures the HTTP client used for making requests to the Groq API
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///    Thrown when the provided API key is null or empty.
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

        builder.Services
            .AddScoped<ChatCompletionClient>()
            .AddScoped<AudioClient>()
            .AddScoped<VisionClient>()
            .AddScoped<ToolClient>()
            .AddScoped<LlmTextProvider>()
            ;

        builder.AddGroqHttpClient(apiKey);
        return builder;
    }


    /// <summary>
    ///     Configures and registers the HTTP client used for interacting with the Groq API.
    /// </summary>
    /// <param name="builder">
    ///     The web application builder to which the Groq HTTP client will be added.
    /// </param>
    /// <param name="apiKey">
    ///    The API key used for authorization with the Groq API.
    /// </param>
    /// <returns>
    ///     The modified <see cref="TBuilder" /> instance.
    /// </returns>
    /// <remarks>
    ///    This method sets up the HTTP client with the base address of the Groq API and configures
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///    Thrown when the provided API key is null or empty.
    /// </exception>
    private static TBuilder AddGroqHttpClient<TBuilder>(this TBuilder builder, string apiKey)
        where TBuilder : IHostApplicationBuilder
    {
        ArgumentException.ThrowIfNullOrEmpty(apiKey);

        builder.Services.AddHttpClient<ChatCompletionClient>(client =>
        {
            client.BaseAddress = new Uri(Endpoints.BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }).AddStandardResilienceHandler();

        builder.Services.AddHttpClient<AudioClient>(client =>
        {
            client.BaseAddress = new Uri(Endpoints.BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }).AddStandardResilienceHandler();

        return builder;
    }
}
