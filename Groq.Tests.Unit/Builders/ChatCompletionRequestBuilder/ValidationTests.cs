namespace Groq.Tests.Unit.Builders.ChatCompletionRequestBuilder;

/// <summary>
///     Tests for ChatCompletionRequestBuilder validation logic.
/// </summary>
public class ValidationTests
{
    #region Required Parameters Validation

    [Fact]
    public void Builder_Should_Throw_When_Model_Not_Set()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithMessages("Test message");

        // Act & Assert
        var exception = Should.Throw<InvalidOperationException>(() => builder.Build());
        exception.Message.ShouldContain("Model is required");
    }

    [Fact]
    public void Builder_Should_Throw_When_Messages_Not_Set()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b");

        // Act & Assert
        var exception = Should.Throw<InvalidOperationException>(() => builder.Build());
        exception.Message.ShouldContain("Messages are required");
    }

    [Fact]
    public void Builder_Should_Throw_When_Both_Model_And_Messages_Not_Set()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder();

        // Act & Assert
        var exception = Should.Throw<InvalidOperationException>(() => builder.Build());
        exception.Message.ShouldContain("Model is required");
    }

    [Fact]
    public void Builder_Should_Throw_When_Model_Is_Empty_String()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("")
            .WithMessages("Test message");

        // Act & Assert
        var exception = Should.Throw<InvalidOperationException>(() => builder.Build());
        exception.Message.ShouldContain("Model is required");
    }

    #endregion

    #region Messages Parameter Validation

    [Fact]
    public void Builder_Should_Accept_Empty_User_Prompt()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("");

        // Act
        var request = builder.Build();

        // Assert
        var messages = request["messages"]!.AsArray();
        messages[0]!["content"]!.GetValue<string>().ShouldBeEmpty();
    }

    [Fact]
    public void Builder_Should_Accept_Null_System_Prompt()
    {
        // Arrange
        var userPrompt = "Hello";
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages(userPrompt);

        // Act
        var request = builder.Build();

        // Assert
        var messages = request["messages"]!.AsArray();
        messages.Count.ShouldBe(1); // Only user message should be added
        messages[0]!["role"]!.GetValue<string>().ShouldBe("user");
    }

    [Fact]
    public void Builder_Should_Accept_Empty_System_Prompt()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("User message", "");

        // Act
        var request = builder.Build();

        // Assert
        var messages = request["messages"]!.AsArray();
        messages.Count.ShouldBe(2);
        messages[0]!["role"]!.GetValue<string>().ShouldBe("system");
        messages[0]!["content"]!.GetValue<string>().ShouldBeEmpty();
    }

    [Fact]
    public void Builder_Should_Handle_Very_Long_User_Prompt()
    {
        // Arrange
        var longPrompt = new string('a', 100000); // 100k characters
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages(longPrompt);

        // Act
        var request = builder.Build();

        // Assert
        var messages = request["messages"]!.AsArray();
        messages[0]!["content"]!.GetValue<string>().Length.ShouldBe(100000);
    }

    [Fact]
    public void Builder_Should_Handle_Special_Characters_In_Messages()
    {
        // Arrange
        var specialPrompt = "Test\n\r\t\"'\\<>{}[]@#$%^&*()";
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages(specialPrompt);

        // Act
        var request = builder.Build();

        // Assert
        var messages = request["messages"]!.AsArray();
        messages[0]!["content"]!.GetValue<string>().ShouldBe(specialPrompt);
    }

    [Fact]
    public void Builder_Should_Handle_Unicode_In_Messages()
    {
        // Arrange
        var unicodePrompt = "Hello 世界 🌍 مرحبا";
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages(unicodePrompt);

        // Act
        var request = builder.Build();

        // Assert
        var messages = request["messages"]!.AsArray();
        messages[0]!["content"]!.GetValue<string>().ShouldBe(unicodePrompt);
    }

    #endregion

    #region ResponseFormat Validation

    [Fact]
    public void Builder_Should_Throw_When_ResponseFormat_Is_Invalid_Json()
    {
        // Arrange
        const string invalidJson = "{ this is not valid json }";
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test");

        // Act & Assert
        Should.Throw<JsonException>(() => builder.WithResponseFormat(invalidJson));
    }

    [Fact]
    public void Builder_Should_Throw_When_ResponseFormat_Is_Empty_String()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test");

        // Act & Assert
        Should.Throw<JsonException>(() => builder.WithResponseFormat(""));
    }

    [Fact]
    public void Builder_Should_Throw_When_ResponseFormat_Is_Null()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test");

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => builder.WithResponseFormat(null!));
    }

    [Fact]
    public void Builder_Should_Accept_Valid_ResponseFormat_Json()
    {
        // Arrange
        const string validJson = "{\"type\":\"object\",\"properties\":{\"name\":{\"type\":\"string\"}}}";
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithResponseFormat(validJson);

        // Act
        var request = builder.Build();

        // Assert
        request.ShouldContainKey("response_format");
        request["response_format"]!["json_schema"].ShouldNotBeNull();
    }

    #endregion

    #region Numeric Parameter Boundary Validation

    [Fact]
    public void Builder_Should_Accept_Zero_Temperature()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithTemperature(0.0);

        // Act
        var request = builder.Build();

        // Assert
        request["temperature"]!.GetValue<double>().ShouldBe(0.0);
    }

    [Fact]
    public void Builder_Should_Accept_Maximum_Temperature()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithTemperature(2.0);

        // Act
        var request = builder.Build();

        // Assert
        request["temperature"]!.GetValue<double>().ShouldBe(2.0);
    }

    [Fact]
    public void Builder_Should_Accept_Negative_Penalties()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithFrequencyPenalty(-2.0)
            .WithPresencePenalty(-2.0);

        // Act
        var request = builder.Build();

        // Assert
        request["frequency_penalty"]!.GetValue<double>().ShouldBe(-2.0);
        request["presence_penalty"]!.GetValue<double>().ShouldBe(-2.0);
    }

    [Fact]
    public void Builder_Should_Accept_Positive_Penalties()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithFrequencyPenalty(2.0)
            .WithPresencePenalty(2.0);

        // Act
        var request = builder.Build();

        // Assert
        request["frequency_penalty"]!.GetValue<double>().ShouldBe(2.0);
        request["presence_penalty"]!.GetValue<double>().ShouldBe(2.0);
    }

    [Fact]
    public void Builder_Should_Accept_Zero_For_Integer_Parameters()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithN(0)
            .WithMaxCompletionTokens(0)
            .WithTopLogprobs(0);

        // Act
        var request = builder.Build();

        // Assert
        request["n"]!.GetValue<int>().ShouldBe(0);
        request["max_completion_tokens"]!.GetValue<int>().ShouldBe(0);
        request["top_logprobs"]!.GetValue<int>().ShouldBe(0);
    }

    [Fact]
    public void Builder_Should_Accept_Negative_Seed()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithSeed(-12345);

        // Act
        var request = builder.Build();

        // Assert
        request["seed"]!.GetValue<int>().ShouldBe(-12345);
    }

    [Fact]
    public void Builder_Should_Accept_Very_Large_Integer_Values()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithMaxCompletionTokens(int.MaxValue)
            .WithSeed(int.MaxValue);

        // Act
        var request = builder.Build();

        // Assert
        request["max_completion_tokens"]!.GetValue<int>().ShouldBe(int.MaxValue);
        request["seed"]!.GetValue<int>().ShouldBe(int.MaxValue);
    }

    #endregion

    #region String Parameter Validation

    [Fact]
    public void Builder_Should_Accept_Special_Characters_In_String_Parameters()
    {
        // Arrange
        var specialString = "test@#$%^&*(){}[]";
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test")
            .WithUser(specialString)
            .WithCitationOptions(specialString);

        // Act
        var request = builder.Build();

        // Assert
        request["user"]!.GetValue<string>().ShouldBe(specialString);
        request["citation_options"]!.GetValue<string>().ShouldBe(specialString);
    }

    [Fact]
    public void Builder_Should_Handle_Very_Long_Model_Name()
    {
        // Arrange
        var longModelName = new string('m', 1000);
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel(longModelName)
            .WithMessages("Test");

        // Act
        var request = builder.Build();

        // Assert
        request["model"]!.GetValue<string>().Length.ShouldBe(1000);
    }

    #endregion

    #region Builder State Validation

    [Fact]
    public void Builder_Should_Not_Allow_Multiple_Builds()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithModel("llama-3.1-8b")
            .WithMessages("Test");

        // Act - First build succeeds
        var request1 = builder.Build();

        // Assert - First build is valid
        request1.ShouldNotBeNull();
        request1["model"]!.GetValue<string>().ShouldBe("llama-3.1-8b");

        // Act & Assert - Second build should throw exception
        Should.Throw<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void Builder_Should_Maintain_State_After_Failed_Build()
    {
        // Arrange
        var builder = new Core.Builders.ChatCompletionRequestBuilder()
            .WithMessages("Test");

        // Act - First build fails
        Should.Throw<InvalidOperationException>(() => builder.Build());

        // Add model and try again
        builder.WithModel("llama-3.1-8b");
        var request = builder.Build();

        // Assert
        request.ShouldNotBeNull();
        request.ShouldContainKey("model");
        request.ShouldContainKey("messages");
    }

    #endregion
}
