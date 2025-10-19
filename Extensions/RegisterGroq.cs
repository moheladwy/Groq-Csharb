using GroqApiLibrary.Clients;
using GroqApiLibrary.Providers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GroqApiLibrary.Settings;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace GroqApiLibrary.Extensions;

public static class RegisterGroq
{
  /// <summary>
  ///     Adds Groq API-related services to the dependency injection container of the application.
  /// </summary>
  /// <param name="builder">
  ///     The builder to add the Groq API-related services to.
  /// </param>
  /// <remarks>
  ///     This method registers the <see cref="ChatCompletionClient" /> and
  ///     <see cref="LlmTextProvider" /> services into the application's service collection.
  ///     These services are essential for enabling Groq API functionalities.
  /// </remarks>
  /// <seealso cref="ChatCompletionClient" />
  /// <seealso cref="LlmTextProvider" />
  public static TBuilder AddGroqApiServices<TBuilder>(this TBuilder builder)
    where TBuilder : IHostApplicationBuilder
  {
    builder.Services
        .AddScoped<ChatCompletionClient>()
        .AddScoped<LlmTextProvider>()
        ;

    builder.AddGroqHttpClient();
    return builder;
  }


  /// <summary>
  ///     Configures and registers the HTTP client used for interacting with the Groq API.
  /// </summary>
  /// <param name="builder">
  ///     The web application builder to which the Groq HTTP client will be added.
  /// </param>
  /// <returns>
  ///     The modified <see cref="TBuilder" /> instance.
  /// </returns>
  /// <remarks>
  ///     This method initializes an HTTP client with a base address and authorization header
  ///     using the API key from the <see cref="GroqSettings" /> configuration.
  /// </remarks>
  public static TBuilder AddGroqHttpClient<TBuilder>(this TBuilder builder)
    where TBuilder : IHostApplicationBuilder
  {
    var settings = builder.Configuration
        .GetSection(Credentials.Section)
        .Get<Credentials>()!;

    builder.Services.AddHttpClient<ChatCompletionClient>(client =>
    {
      client.BaseAddress = new Uri(Endpoints.BaseUrl);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.ApiKey);
    }).AddStandardResilienceHandler();

    return builder;
  }
}
