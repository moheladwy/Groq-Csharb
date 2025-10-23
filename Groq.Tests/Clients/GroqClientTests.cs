using Groq.Core.Clients;
using Groq.Core.Configurations;
using Groq.Core.Interfaces;
using Groq.Core.Providers;
using Moq;

namespace Groq.Tests.Clients;

/// <summary>
/// Unit tests for the GroqClient class.
/// </summary>
public class GroqClientTests
{
    [Fact]
    public void GroqClient_ConstructorWithOptions_InitializesAllClients()
    {
        // Arrange
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            Model = "test-model"
        };

        // Act
        var client = new GroqClient(options);

        // Assert
        Assert.NotNull(client.Chat);
        Assert.NotNull(client.Audio);
        Assert.NotNull(client.Vision);
        Assert.NotNull(client.Tools);
        Assert.NotNull(client.LlmTextProvider);
    }

    [Fact]
    public void GroqClient_ConstructorWithOptions_NullOptions_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GroqClient((GroqOptions)null!));
    }

    [Fact]
    public void GroqClient_ConstructorWithOptions_NullApiKey_ThrowsArgumentException()
    {
        // Arrange
        var options = new GroqOptions
        {
            ApiKey = null!
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new GroqClient(options));
    }

    [Fact]
    public void GroqClient_ConstructorWithOptions_EmptyApiKey_ThrowsArgumentException()
    {
        // Arrange
        var options = new GroqOptions
        {
            ApiKey = string.Empty
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new GroqClient(options));
    }

    [Fact]
    public void GroqClient_ConstructorWithDependencies_InitializesAllClients()
    {
        // Arrange
        var mockHttpClient = new HttpClient();
        var chatClient = new ChatCompletionClient(mockHttpClient);
        var audioClient = new AudioClient(mockHttpClient);
        var visionClient = new VisionClient(chatClient);
        var toolClient = new ToolClient(chatClient);
        var mockLlmProvider = Mock.Of<ILlmTextProvider>();

        // Act
        var client = new GroqClient(
            chatClient,
            audioClient,
            visionClient,
            toolClient,
            mockLlmProvider);

        // Assert
        Assert.Same(chatClient, client.Chat);
        Assert.Same(audioClient, client.Audio);
        Assert.Same(visionClient, client.Vision);
        Assert.Same(toolClient, client.Tools);
        Assert.Same(mockLlmProvider, client.LlmTextProvider);
    }

    [Fact]
    public void GroqClient_ConstructorWithDependencies_NullChatClient_ThrowsArgumentNullException()
    {
        // Arrange
        var mockHttpClient = new HttpClient();
        var audioClient = new AudioClient(mockHttpClient);
        var chatClient = new ChatCompletionClient(mockHttpClient);
        var visionClient = new VisionClient(chatClient);
        var toolClient = new ToolClient(chatClient);
        var mockLlmProvider = Mock.Of<ILlmTextProvider>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GroqClient(
            null!,
            audioClient,
            visionClient,
            toolClient,
            mockLlmProvider));
    }

    [Fact]
    public void GroqClient_ConstructorWithDependencies_NullAudioClient_ThrowsArgumentNullException()
    {
        // Arrange
        var mockHttpClient = new HttpClient();
        var chatClient = new ChatCompletionClient(mockHttpClient);
        var visionClient = new VisionClient(chatClient);
        var toolClient = new ToolClient(chatClient);
        var mockLlmProvider = Mock.Of<ILlmTextProvider>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GroqClient(
            chatClient,
            null!,
            visionClient,
            toolClient,
            mockLlmProvider));
    }

    [Fact]
    public void GroqClient_ConstructorWithDependencies_NullVisionClient_ThrowsArgumentNullException()
    {
        // Arrange
        var mockHttpClient = new HttpClient();
        var chatClient = new ChatCompletionClient(mockHttpClient);
        var audioClient = new AudioClient(mockHttpClient);
        var toolClient = new ToolClient(chatClient);
        var mockLlmProvider = Mock.Of<ILlmTextProvider>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GroqClient(
            chatClient,
            audioClient,
            null!,
            toolClient,
            mockLlmProvider));
    }

    [Fact]
    public void GroqClient_ConstructorWithDependencies_NullToolClient_ThrowsArgumentNullException()
    {
        // Arrange
        var mockHttpClient = new HttpClient();
        var chatClient = new ChatCompletionClient(mockHttpClient);
        var audioClient = new AudioClient(mockHttpClient);
        var visionClient = new VisionClient(chatClient);
        var mockLlmProvider = Mock.Of<ILlmTextProvider>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GroqClient(
            chatClient,
            audioClient,
            visionClient,
            null!,
            mockLlmProvider));
    }

    [Fact]
    public void GroqClient_ConstructorWithDependencies_NullLlmTextProvider_ThrowsArgumentNullException()
    {
        // Arrange
        var mockHttpClient = new HttpClient();
        var chatClient = new ChatCompletionClient(mockHttpClient);
        var audioClient = new AudioClient(mockHttpClient);
        var visionClient = new VisionClient(chatClient);
        var toolClient = new ToolClient(chatClient);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GroqClient(
            chatClient,
            audioClient,
            visionClient,
            toolClient,
            null!));
    }

    [Fact]
    public void GroqClient_ConstructorWithOptions_UsesCustomBaseUrl()
    {
        // Arrange
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            BaseUrl = "https://custom.api.com/v1/"
        };

        // Act
        var client = new GroqClient(options);

        // Assert
        Assert.NotNull(client);
    }

    [Fact]
    public void GroqClient_ConstructorWithOptions_UsesCustomTimeout()
    {
        // Arrange
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            Timeout = TimeSpan.FromSeconds(60)
        };

        // Act
        var client = new GroqClient(options);

        // Assert
        Assert.NotNull(client);
    }

    [Theory]
    [InlineData("llama-3.3-70b-versatile")]
    [InlineData("openai/gpt-oss-120b")]
    [InlineData("custom-model")]
    public void GroqClient_ConstructorWithOptions_UsesSpecifiedModel(string modelId)
    {
        // Arrange
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            Model = modelId
        };

        // Act
        var client = new GroqClient(options);

        // Assert
        Assert.NotNull(client);
        Assert.NotNull(client.LlmTextProvider);
    }
}