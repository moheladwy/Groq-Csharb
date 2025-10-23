using Groq.Core.Configurations;

namespace Groq.Tests.Configurations;

/// <summary>
/// Unit tests for the GroqOptions configuration class.
/// </summary>
public class GroqOptionsTests
{
    [Fact]
    public void GroqOptions_DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var options = new GroqOptions
        {
            ApiKey = "test-api-key"
        };

        // Assert
        Assert.Equal("test-api-key", options.ApiKey);
        Assert.Equal(Endpoints.BaseUrl, options.BaseUrl);
        Assert.Null(options.Model);
        Assert.Equal(TimeSpan.FromSeconds(100), options.Timeout);
        Assert.Equal(3, options.MaxRetries);
        Assert.Equal(TimeSpan.FromSeconds(2), options.Delay);
        Assert.Equal(TimeSpan.FromSeconds(20), options.MaxDelay);
    }

    [Fact]
    public void GroqOptions_CustomValues_AreSetCorrectly()
    {
        // Arrange & Act
        var options = new GroqOptions
        {
            ApiKey = "custom-api-key",
            BaseUrl = "https://custom.api.com/",
            Model = "custom-model",
            Timeout = TimeSpan.FromSeconds(60),
            MaxRetries = 5,
            Delay = TimeSpan.FromSeconds(1),
            MaxDelay = TimeSpan.FromSeconds(30)
        };

        // Assert
        Assert.Equal("custom-api-key", options.ApiKey);
        Assert.Equal("https://custom.api.com/", options.BaseUrl);
        Assert.Equal("custom-model", options.Model);
        Assert.Equal(TimeSpan.FromSeconds(60), options.Timeout);
        Assert.Equal(5, options.MaxRetries);
        Assert.Equal(TimeSpan.FromSeconds(1), options.Delay);
        Assert.Equal(TimeSpan.FromSeconds(30), options.MaxDelay);
    }

    [Fact]
    public void GroqOptions_NullModel_IsAllowed()
    {
        // Arrange & Act
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            Model = null
        };

        // Assert
        Assert.Null(options.Model);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    public void GroqOptions_MaxRetries_AcceptsValidValues(int maxRetries)
    {
        // Arrange & Act
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            MaxRetries = maxRetries
        };

        // Assert
        Assert.Equal(maxRetries, options.MaxRetries);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(30)]
    [InlineData(100)]
    [InlineData(300)]
    public void GroqOptions_Timeout_AcceptsValidDurations(int seconds)
    {
        // Arrange
        var timeout = TimeSpan.FromSeconds(seconds);

        // Act
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            Timeout = timeout
        };

        // Assert
        Assert.Equal(timeout, options.Timeout);
    }

    [Theory]
    [InlineData(1, 10)]
    [InlineData(2, 20)]
    [InlineData(5, 30)]
    public void GroqOptions_DelayConfiguration_WorksCorrectly(int delaySec, int maxDelaySec)
    {
        // Arrange
        var delay = TimeSpan.FromSeconds(delaySec);
        var maxDelay = TimeSpan.FromSeconds(maxDelaySec);

        // Act
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            Delay = delay,
            MaxDelay = maxDelay
        };

        // Assert
        Assert.Equal(delay, options.Delay);
        Assert.Equal(maxDelay, options.MaxDelay);
    }

    [Fact]
    public void GroqOptions_EmptyApiKey_CanBeSet()
    {
        // Arrange & Act
        var options = new GroqOptions
        {
            ApiKey = string.Empty
        };

        // Assert
        Assert.Equal(string.Empty, options.ApiKey);
    }

    [Fact]
    public void GroqOptions_BaseUrlWithTrailingSlash_IsPreserved()
    {
        // Arrange & Act
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            BaseUrl = "https://api.example.com/"
        };

        // Assert
        Assert.Equal("https://api.example.com/", options.BaseUrl);
    }

    [Fact]
    public void GroqOptions_BaseUrlWithoutTrailingSlash_IsPreserved()
    {
        // Arrange & Act
        var options = new GroqOptions
        {
            ApiKey = "test-api-key",
            BaseUrl = "https://api.example.com"
        };

        // Assert
        Assert.Equal("https://api.example.com", options.BaseUrl);
    }
}