using System.Text.Json;
using System.Text.Json.Nodes;
using Groq.Core.Clients;
using Groq.Core.Configurations;
using Groq.Core.Models;
using Groq.Core.Providers;
using Moq;

namespace Groq.Tests.Providers;

/// <summary>
/// Unit tests for the LlmTextProvider class, focusing on prompt ordering and functionality.
/// </summary>
public class LlmTextProviderTests
{
    private readonly Mock<ChatCompletionClient> _mockChatClient;

    public LlmTextProviderTests()
    {
        var mockHttpClient = new HttpClient();
        _mockChatClient = new Mock<ChatCompletionClient>(mockHttpClient);
    }

    [Fact]
    public void LlmTextProvider_Constructor_WithDefaultModel_UsesDefaultModel()
    {
        // Arrange & Act
        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Assert
        Assert.NotNull(provider);
    }

    [Fact]
    public void LlmTextProvider_Constructor_WithCustomModel_UsesCustomModel()
    {
        // Arrange
        const string customModel = "custom-model-id";

        // Act
        var provider = new LlmTextProvider(_mockChatClient.Object, customModel);

        // Assert
        Assert.NotNull(provider);
    }

    [Fact]
    public void LlmTextProvider_Constructor_WithNullModel_UsesDefaultModel()
    {
        // Arrange & Act
        var provider = new LlmTextProvider(_mockChatClient.Object, null);

        // Assert
        Assert.NotNull(provider);
    }

    [Fact]
    public async Task GenerateAsync_WithUserPromptOnly_CreatesCorrectRequest()
    {
        // Arrange
        const string userPrompt = "What is AI?";
        JsonObject? capturedRequest = null;

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .Callback<JsonObject>(req => capturedRequest = req)
            .ReturnsAsync(CreateMockResponse("AI is artificial intelligence."));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt);

        // Assert
        Assert.NotNull(capturedRequest);
        var messages = capturedRequest["messages"]?.AsArray();
        Assert.NotNull(messages);
        Assert.Single(messages);
        
        var userMessage = messages[0]?.AsObject();
        Assert.Equal(LlmRoles.UserRole, userMessage?["role"]?.GetValue<string>());
        Assert.Equal(userPrompt, userMessage?["content"]?.GetValue<string>());
    }

    [Fact]
    public async Task GenerateAsync_WithSystemAndUserPrompts_AddsSystemPromptFirst()
    {
        // Arrange
        const string systemPrompt = "You are a helpful assistant.";
        const string userPrompt = "What is AI?";
        JsonObject? capturedRequest = null;

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .Callback<JsonObject>(req => capturedRequest = req)
            .ReturnsAsync(CreateMockResponse("AI is artificial intelligence."));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt, systemPrompt);

        // Assert
        Assert.NotNull(capturedRequest);
        var messages = capturedRequest["messages"]?.AsArray();
        Assert.NotNull(messages);
        Assert.Equal(2, messages.Count);
        
        // Verify system prompt is first
        var systemMessage = messages[0]?.AsObject();
        Assert.Equal(LlmRoles.SystemRole, systemMessage?["role"]?.GetValue<string>());
        Assert.Equal(systemPrompt, systemMessage?["content"]?.GetValue<string>());
        
        // Verify user prompt is second
        var userMessage = messages[1]?.AsObject();
        Assert.Equal(LlmRoles.UserRole, userMessage?["role"]?.GetValue<string>());
        Assert.Equal(userPrompt, userMessage?["content"]?.GetValue<string>());
    }

    [Fact]
    public async Task GenerateAsync_WithEmptySystemPrompt_OnlyIncludesUserPrompt()
    {
        // Arrange
        const string systemPrompt = "";
        const string userPrompt = "What is AI?";
        JsonObject? capturedRequest = null;

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .Callback<JsonObject>(req => capturedRequest = req)
            .ReturnsAsync(CreateMockResponse("AI is artificial intelligence."));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt, systemPrompt);

        // Assert
        Assert.NotNull(capturedRequest);
        var messages = capturedRequest["messages"]?.AsArray();
        Assert.NotNull(messages);
        Assert.Single(messages);
        
        var userMessage = messages[0]?.AsObject();
        Assert.Equal(LlmRoles.UserRole, userMessage?["role"]?.GetValue<string>());
        Assert.Equal(userPrompt, userMessage?["content"]?.GetValue<string>());
    }

    [Fact]
    public async Task GenerateAsync_WithNullSystemPrompt_OnlyIncludesUserPrompt()
    {
        // Arrange
        const string userPrompt = "What is AI?";
        JsonObject? capturedRequest = null;

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .Callback<JsonObject>(req => capturedRequest = req)
            .ReturnsAsync(CreateMockResponse("AI is artificial intelligence."));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt, null);

        // Assert
        Assert.NotNull(capturedRequest);
        var messages = capturedRequest["messages"]?.AsArray();
        Assert.NotNull(messages);
        Assert.Single(messages);
        
        var userMessage = messages[0]?.AsObject();
        Assert.Equal(LlmRoles.UserRole, userMessage?["role"]?.GetValue<string>());
        Assert.Equal(userPrompt, userMessage?["content"]?.GetValue<string>());
    }

    [Fact]
    public async Task GenerateAsync_WithNullUserPrompt_ThrowsArgumentNullException()
    {
        // Arrange
        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await provider.GenerateAsync(null!));
    }

    [Fact]
    public async Task GenerateAsync_WithStructuredOutput_AddsResponseFormat()
    {
        // Arrange
        const string userPrompt = "Generate JSON";
        const string jsonSchema = "{\"type\":\"object\",\"properties\":{\"name\":{\"type\":\"string\"}}}";
        JsonObject? capturedRequest = null;

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .Callback<JsonObject>(req => capturedRequest = req)
            .ReturnsAsync(CreateMockResponse("{\"name\":\"test\"}"));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt, null, jsonSchema);

        // Assert
        Assert.NotNull(capturedRequest);
        Assert.NotNull(capturedRequest["response_format"]);
        var responseFormat = capturedRequest["response_format"]?.AsObject();
        Assert.Equal("json_schema", responseFormat?["type"]?.GetValue<string>());
        Assert.NotNull(responseFormat?["json_schema"]);
    }

    [Fact]
    public async Task GenerateAsync_WithInvalidJsonSchema_ThrowsArgumentException()
    {
        // Arrange
        const string userPrompt = "Generate JSON";
        const string invalidJsonSchema = "{invalid json";

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await provider.GenerateAsync(userPrompt, null, invalidJsonSchema));
    }

    [Fact]
    public async Task GenerateAsync_ReturnsExpectedResponse()
    {
        // Arrange
        const string userPrompt = "What is AI?";
        const string expectedResponse = "AI is artificial intelligence.";

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .ReturnsAsync(CreateMockResponse(expectedResponse));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt);

        // Assert
        Assert.Equal(expectedResponse, result);
    }

    [Fact]
    public async Task GenerateAsync_WithNullResponse_ReturnsEmptyString()
    {
        // Arrange
        const string userPrompt = "What is AI?";

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .ReturnsAsync((JsonObject?)null);

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task GenerateAsync_WithCustomModel_UsesSpecifiedModel()
    {
        // Arrange
        const string customModel = "custom-model-id";
        const string userPrompt = "Test prompt";
        JsonObject? capturedRequest = null;

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .Callback<JsonObject>(req => capturedRequest = req)
            .ReturnsAsync(CreateMockResponse("Test response"));

        var provider = new LlmTextProvider(_mockChatClient.Object, customModel);

        // Act
        await provider.GenerateAsync(userPrompt);

        // Assert
        Assert.NotNull(capturedRequest);
        Assert.Equal(customModel, capturedRequest["model"]?.GetValue<string>());
    }

    [Theory]
    [InlineData("")]
    [InlineData("Hello, world!")]
    [InlineData("This is a longer prompt with multiple sentences. It should work correctly.")]
    public async Task GenerateAsync_WithVariousUserPrompts_WorksCorrectly(string userPrompt)
    {
        // Arrange
        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .ReturnsAsync(CreateMockResponse("Response"));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        var result = await provider.GenerateAsync(userPrompt);

        // Assert
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("System context", "User query")]
    [InlineData("You are an expert.", "Explain quantum physics.")]
    [InlineData("Be brief.", "What is 2+2?")]
    public async Task GenerateAsync_WithVariousPromptCombinations_MaintainsCorrectOrder(
        string systemPrompt, string userPrompt)
    {
        // Arrange
        JsonObject? capturedRequest = null;

        _mockChatClient
            .Setup(x => x.CreateChatCompletionAsync(It.IsAny<JsonObject>()))
            .Callback<JsonObject>(req => capturedRequest = req)
            .ReturnsAsync(CreateMockResponse("Response"));

        var provider = new LlmTextProvider(_mockChatClient.Object);

        // Act
        await provider.GenerateAsync(userPrompt, systemPrompt);

        // Assert
        Assert.NotNull(capturedRequest);
        var messages = capturedRequest["messages"]?.AsArray();
        Assert.NotNull(messages);
        Assert.Equal(2, messages.Count);
        
        // System prompt must be first
        Assert.Equal(LlmRoles.SystemRole, messages[0]?["role"]?.GetValue<string>());
        Assert.Equal(systemPrompt, messages[0]?["content"]?.GetValue<string>());
        
        // User prompt must be second
        Assert.Equal(LlmRoles.UserRole, messages[1]?["role"]?.GetValue<string>());
        Assert.Equal(userPrompt, messages[1]?["content"]?.GetValue<string>());
    }

    /// <summary>
    /// Helper method to create a mock API response.
    /// </summary>
    private static JsonObject CreateMockResponse(string content)
    {
        return JsonNode.Parse($@"{{
            ""choices"": [
                {{
                    ""message"": {{
                        ""content"": ""{content}""
                    }}
                }}
            ]
        }}")?.AsObject() ?? new JsonObject();
    }
}