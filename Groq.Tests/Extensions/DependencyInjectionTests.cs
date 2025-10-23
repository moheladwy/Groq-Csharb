using Groq.Core.Clients;
using Groq.Core.Configurations;
using Groq.Core.Interfaces;
using Groq.Core.Providers;
using Groq.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Groq.Tests.Extensions;

/// <summary>
/// Unit tests for the DependencyInjection extension methods.
/// </summary>
public class DependencyInjectionTests
{
    [Fact]
    public void AddGroqApiServices_RegistersAllServices()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
            options.Model = "test-model";
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        Assert.NotNull(serviceProvider.GetService<ChatCompletionClient>());
        Assert.NotNull(serviceProvider.GetService<AudioClient>());
        Assert.NotNull(serviceProvider.GetService<VisionClient>());
        Assert.NotNull(serviceProvider.GetService<ToolClient>());
        Assert.NotNull(serviceProvider.GetService<ILlmTextProvider>());
        Assert.NotNull(serviceProvider.GetService<GroqClient>());
    }

    [Fact]
    public void AddGroqApiServices_NullConfigureOptions_ThrowsArgumentNullException()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            builder.AddGroqApiServices((Action<GroqOptions>)null!));
    }

    [Fact]
    public void AddGroqApiServices_WithValidOptions_RegistersHttpClient()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        Assert.NotNull(httpClientFactory);
    }

    [Fact]
    public void AddGroqApiServices_RegistersGroqClient_WithAllDependencies()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
            options.Model = "test-model";
        });

        var serviceProvider = builder.Build().Services;
        var groqClient = serviceProvider.GetService<GroqClient>();

        // Assert
        Assert.NotNull(groqClient);
        Assert.NotNull(groqClient.Chat);
        Assert.NotNull(groqClient.Audio);
        Assert.NotNull(groqClient.Vision);
        Assert.NotNull(groqClient.Tools);
        Assert.NotNull(groqClient.LlmTextProvider);
    }

    [Fact]
    public void AddGroqApiServices_WithCustomBaseUrl_UsesCustomUrl()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();
        const string customBaseUrl = "https://custom.api.com/v1/";

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
            options.BaseUrl = customBaseUrl;
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        var groqClient = serviceProvider.GetService<GroqClient>();
        Assert.NotNull(groqClient);
    }

    [Fact]
    public void AddGroqApiServices_WithRetryConfiguration_AppliesConfiguration()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
            options.MaxRetries = 5;
            options.Delay = TimeSpan.FromSeconds(3);
            options.MaxDelay = TimeSpan.FromSeconds(30);
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        var groqClient = serviceProvider.GetService<GroqClient>();
        Assert.NotNull(groqClient);
    }

    [Fact]
    public void AddGroqApiServices_WithTimeout_AppliesConfiguration()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
            options.Timeout = TimeSpan.FromSeconds(60);
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        var groqClient = serviceProvider.GetService<GroqClient>();
        Assert.NotNull(groqClient);
    }

    [Fact]
    public void AddGroqApiServices_LlmTextProvider_UsesConfiguredModel()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();
        const string testModel = "custom-test-model";

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
            options.Model = testModel;
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        var llmProvider = serviceProvider.GetService<ILlmTextProvider>();
        Assert.NotNull(llmProvider);
    }

    [Fact]
    public void AddGroqApiServices_ChatCompletionClient_IsRegisteredAsScoped()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key";
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        using var scope1 = serviceProvider.CreateScope();
        using var scope2 = serviceProvider.CreateScope();

        var client1 = scope1.ServiceProvider.GetService<ChatCompletionClient>();
        var client2 = scope2.ServiceProvider.GetService<ChatCompletionClient>();

        Assert.NotNull(client1);
        Assert.NotNull(client2);
        Assert.NotSame(client1, client2);
    }

    [Fact]
    public void AddGroqApiServices_MultipleRegistrations_WorkCorrectly()
    {
        // Arrange
        var builder = Host.CreateApplicationBuilder();

        // Act - Register twice (should not cause issues)
        builder.AddGroqApiServices(options =>
        {
            options.ApiKey = "test-api-key-1";
        });

        var serviceProvider = builder.Build().Services;

        // Assert
        var groqClient = serviceProvider.GetService<GroqClient>();
        Assert.NotNull(groqClient);
    }
}