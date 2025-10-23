using Groq.Core.Configurations;

namespace Groq.Tests.Configurations;

/// <summary>
/// Unit tests for the Endpoints static configuration class.
/// </summary>
public class EndpointsTests
{
    [Fact]
    public void Endpoints_BaseUrl_IsCorrect()
    {
        // Assert
        Assert.Equal("https://api.groq.com/openai/v1/", Endpoints.BaseUrl);
    }

    [Fact]
    public void Endpoints_ChatCompletionsEndpoint_IsCorrect()
    {
        // Assert
        Assert.Equal("chat/completions", Endpoints.ChatCompletionsEndpoint);
    }

    [Fact]
    public void Endpoints_TranscriptionsEndpoint_IsCorrect()
    {
        // Assert
        Assert.Equal("audio/transcriptions", Endpoints.TranscriptionsEndpoint);
    }

    [Fact]
    public void Endpoints_TranslationsEndpoint_IsCorrect()
    {
        // Assert
        Assert.Equal("audio/translations", Endpoints.TranslationsEndpoint);
    }

    [Fact]
    public void Endpoints_TextToSpeechEndpoint_IsCorrect()
    {
        // Assert
        Assert.Equal("audio/speech", Endpoints.TextToSpeechEndpoint);
    }

    [Fact]
    public void Endpoints_GetAllModelsEndpoint_IsCorrect()
    {
        // Assert
        Assert.Equal("models", Endpoints.GetAllModelsEndpoint);
    }

    [Fact]
    public void Endpoints_AllEndpoints_AreNotNull()
    {
        // Assert
        Assert.NotNull(Endpoints.BaseUrl);
        Assert.NotNull(Endpoints.ChatCompletionsEndpoint);
        Assert.NotNull(Endpoints.TranscriptionsEndpoint);
        Assert.NotNull(Endpoints.TranslationsEndpoint);
        Assert.NotNull(Endpoints.TextToSpeechEndpoint);
        Assert.NotNull(Endpoints.GetAllModelsEndpoint);
    }

    [Fact]
    public void Endpoints_AllEndpoints_AreNotEmpty()
    {
        // Assert
        Assert.NotEmpty(Endpoints.BaseUrl);
        Assert.NotEmpty(Endpoints.ChatCompletionsEndpoint);
        Assert.NotEmpty(Endpoints.TranscriptionsEndpoint);
        Assert.NotEmpty(Endpoints.TranslationsEndpoint);
        Assert.NotEmpty(Endpoints.TextToSpeechEndpoint);
        Assert.NotEmpty(Endpoints.GetAllModelsEndpoint);
    }
}