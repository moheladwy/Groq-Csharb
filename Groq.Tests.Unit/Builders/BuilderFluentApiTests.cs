using System.Text.Json;
using Groq.Core.Models;

namespace Groq.Tests.Unit.Builders;

/// <summary>
///     Tests for ChatCompletionRequestBuilder fluent API functionality.
///     Tests ID: BF-001 to BF-005
/// </summary>
public class BuilderFluentApiTests
{
    // Const values corresponding to private fields in ChatCompletionRequestBuilder
    private const string CitationOptionsValue = "enabled";
    private const bool DisableToolValidationValue = true;
    private const double FrequencyPenaltyValue = 0.5;
    private const bool IncludeReasoningValue = true;
    private const bool LogprobsValue = true;
    private const int MaxCompletionTokensValue = 1024;
    private const int MaxTokensValue = 2048;
    private const int NValue = 1;
    private const bool ParallelToolCallsValue = false;
    private const double PresencePenaltyValue = 0.3;
    private const string ReasoningEffortValue = "medium";
    private const string ReasoningFormatValue = "parsed";
    private const int SeedValue = 12345;
    private const string ServiceTierValue = "on_demand";
    private const bool StoreValue = true;
    private const bool StreamValue = false;
    private const double TemperatureValue = 0.7;
    private const int TopLogprobsValue = 5;
    private const double TopPValue = 0.9;
    private const string UserValue = "user-123";
    private const string UserPrompt = "Hello, how are you?";
    private const string SystemPrompt = "You are a helpful assistant.";
    private const int MaxTokens = 1024;
    private const double Temperature = 0.7;
    private const double TopP = 0.9;

    // ReSharper disable once InconsistentNaming
    private static readonly string DefaultModel = ChatModels.OPENAI_GPT_OSS_120B.Id;

    [Fact]
    public void Builder_Should_Support_Method_Chaining()
    {
        // Arrange & Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt, SystemPrompt)
            .WithTemperature(Temperature)
            .WithMaxCompletionTokens(MaxTokens)
            .WithTopP(TopP)
            .Build();

        // Assert
        request.ShouldContainKey("model");
        request.ShouldContainKey("messages");
        request.ShouldContainKey("temperature");
        request.ShouldContainKey("max_completion_tokens");
        request.ShouldContainKey("top_p");
    }

    [Fact]
    public void Builder_Should_Return_Builder_Instance_From_All_Methods()
    {
        // Arrange
        var builder = new ChatCompletionRequestBuilder();
        var testJsonObject = new JsonObject { ["test"] = "value" };
        var testJsonArray = new JsonArray { "item1", "item2" };
        var testJsonNode = JsonNode.Parse("\"test\"");

        // Act & Assert - Core Methods
        builder.WithModel(DefaultModel).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithMessages(UserPrompt).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithMessages(UserPrompt, SystemPrompt).ShouldBeOfType<ChatCompletionRequestBuilder>();

        // Optional Parameters
        builder.WithCitationOptions(CitationOptionsValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithCompoundCustom(testJsonObject).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithDisableToolValidation(DisableToolValidationValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithDocuments(testJsonArray).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithFrequencyPenalty(FrequencyPenaltyValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithIncludeReasoning(IncludeReasoningValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithLogitBias(testJsonObject).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithLogprobs(LogprobsValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithMaxCompletionTokens(MaxCompletionTokensValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithMetadata(testJsonObject).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithN(NValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithParallelToolCalls(ParallelToolCallsValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithPresencePenalty(PresencePenaltyValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithReasoningEffort(ReasoningEffortValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithReasoningFormat(ReasoningFormatValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithResponseFormat("{\"type\":\"json_object\"}").ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithSearchSettings(testJsonObject).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithSeed(SeedValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithServiceTier(ServiceTierValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithStop(testJsonNode!).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithStore(StoreValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithStream(StreamValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithStreamOptions(testJsonObject).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithTemperature(TemperatureValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithToolChoice(testJsonNode!).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithTools(testJsonArray).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithTopLogprobs(TopLogprobsValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithTopP(TopPValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithUser(UserValue).ShouldBeOfType<ChatCompletionRequestBuilder>();

        // Deprecated Methods (for backward compatibility)
#pragma warning disable CS0618 // Type or member is obsolete
        builder.WithExcludeDomains(testJsonArray).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithFunctionCall(testJsonNode!).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithFunctions(testJsonArray).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithIncludeDomains(testJsonArray).ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder.WithMaxTokens(MaxTokensValue).ShouldBeOfType<ChatCompletionRequestBuilder>();
#pragma warning restore CS0618 // Type or member is obsolete
    }

    [Fact]
    public void Builder_Should_Build_Complex_Request_With_All_Parameters()
    {
        // Arrange
        var testCompoundCustom = new JsonObject { ["custom_field"] = "custom_value" };
        var testDocuments = new JsonArray { new JsonObject { ["id"] = "doc1", ["content"] = "Document content" } };
        var testLogitBias = new JsonObject { ["50256"] = -100 };
        var testMetadata = new JsonObject { ["user_id"] = "test_user", ["session_id"] = "test_session" };
        var testResponseFormat = "{\"type\":\"json_object\",\"schema\":{\"type\":\"object\"}}";
        var testSearchSettings = new JsonObject { ["max_results"] = 10, ["search_depth"] = "advanced" };
        var testStop = JsonNode.Parse("[\"STOP\",\"END\"]");
        var testStreamOptions = new JsonObject { ["include_usage"] = true };
        var testToolChoice = JsonNode.Parse("\"auto\"");
        var testTools = new JsonArray
        {
            new JsonObject
            {
                ["type"] = "function",
                ["function"] = new JsonObject
                {
                    ["name"] = "get_weather",
                    ["description"] = "Get weather information",
                    ["parameters"] = new JsonObject
                    {
                        ["type"] = "object",
                        ["properties"] = new JsonObject { }
                    }
                }
            }
        };
        var testExcludeDomains = new JsonArray { "example.com", "spam.com" };
        var testFunctionCall = JsonNode.Parse("\"auto\"");
        var testFunctions = new JsonArray
        {
            new JsonObject
            {
                ["name"] = "old_function",
                ["description"] = "Legacy function"
            }
        };
        var testIncludeDomains = new JsonArray { "trusted.com", "verified.com" };

        // Act
#pragma warning disable CS0618 // Type or member is obsolete
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt, SystemPrompt)
            .WithCitationOptions(CitationOptionsValue)
            .WithCompoundCustom(testCompoundCustom)
            .WithDisableToolValidation(DisableToolValidationValue)
            .WithDocuments(testDocuments)
            .WithFrequencyPenalty(FrequencyPenaltyValue)
            .WithIncludeReasoning(IncludeReasoningValue)
            .WithLogitBias(testLogitBias)
            .WithLogprobs(LogprobsValue)
            .WithMaxCompletionTokens(MaxCompletionTokensValue)
            .WithMetadata(testMetadata)
            .WithN(NValue)
            .WithParallelToolCalls(ParallelToolCallsValue)
            .WithPresencePenalty(PresencePenaltyValue)
            .WithReasoningEffort(ReasoningEffortValue)
            .WithReasoningFormat(ReasoningFormatValue)
            .WithResponseFormat(testResponseFormat)
            .WithSearchSettings(testSearchSettings)
            .WithSeed(SeedValue)
            .WithServiceTier(ServiceTierValue)
            .WithStop(testStop!)
            .WithStore(StoreValue)
            .WithStream(StreamValue)
            .WithStreamOptions(testStreamOptions)
            .WithTemperature(TemperatureValue)
            .WithToolChoice(testToolChoice!)
            .WithTools(testTools)
            .WithTopLogprobs(TopLogprobsValue)
            .WithTopP(TopPValue)
            .WithUser(UserValue)
            .WithExcludeDomains(testExcludeDomains)
            .WithFunctionCall(testFunctionCall!)
            .WithFunctions(testFunctions)
            .WithIncludeDomains(testIncludeDomains)
            .WithMaxTokens(MaxTokensValue)
            .Build();
#pragma warning restore CS0618 // Type or member is obsolete

        // Assert - Verify all keys exist
        request.ShouldContainKey("model");
        request.ShouldContainKey("messages");
        request.ShouldContainKey("citation_options");
        request.ShouldContainKey("compound_custom");
        request.ShouldContainKey("disable_tool_validation");
        request.ShouldContainKey("documents");
        request.ShouldContainKey("frequency_penalty");
        request.ShouldContainKey("include_reasoning");
        request.ShouldContainKey("logit_bias");
        request.ShouldContainKey("logprobs");
        request.ShouldContainKey("max_completion_tokens");
        request.ShouldContainKey("metadata");
        request.ShouldContainKey("n");
        request.ShouldContainKey("parallel_tool_calls");
        request.ShouldContainKey("presence_penalty");
        request.ShouldContainKey("reasoning_effort");
        request.ShouldContainKey("reasoning_format");
        request.ShouldContainKey("response_format");
        request.ShouldContainKey("search_settings");
        request.ShouldContainKey("seed");
        request.ShouldContainKey("service_tier");
        request.ShouldContainKey("stop");
        request.ShouldContainKey("store");
        request.ShouldContainKey("stream");
        request.ShouldContainKey("stream_options");
        request.ShouldContainKey("temperature");
        request.ShouldContainKey("tool_choice");
        request.ShouldContainKey("tools");
        request.ShouldContainKey("top_logprobs");
        request.ShouldContainKey("top_p");
        request.ShouldContainKey("user");
        request.ShouldContainKey("exclude_domains");
        request.ShouldContainKey("function_call");
        request.ShouldContainKey("functions");
        request.ShouldContainKey("include_domains");
        request.ShouldContainKey("max_tokens");

        // Assert - Verify all values are correct
        request["model"]!.GetValue<string>().ShouldBe(DefaultModel);
        request["citation_options"]!.GetValue<string>().ShouldBe(CitationOptionsValue);
        request["disable_tool_validation"]!.GetValue<bool>().ShouldBe(DisableToolValidationValue);
        request["frequency_penalty"]!.GetValue<double>().ShouldBe(FrequencyPenaltyValue);
        request["include_reasoning"]!.GetValue<bool>().ShouldBe(IncludeReasoningValue);
        request["logprobs"]!.GetValue<bool>().ShouldBe(LogprobsValue);
        request["max_completion_tokens"]!.GetValue<int>().ShouldBe(MaxCompletionTokensValue);
        request["n"]!.GetValue<int>().ShouldBe(NValue);
        request["parallel_tool_calls"]!.GetValue<bool>().ShouldBe(ParallelToolCallsValue);
        request["presence_penalty"]!.GetValue<double>().ShouldBe(PresencePenaltyValue);
        request["reasoning_effort"]!.GetValue<string>().ShouldBe(ReasoningEffortValue);
        request["reasoning_format"]!.GetValue<string>().ShouldBe(ReasoningFormatValue);
        request["seed"]!.GetValue<int>().ShouldBe(SeedValue);
        request["service_tier"]!.GetValue<string>().ShouldBe(ServiceTierValue);
        request["store"]!.GetValue<bool>().ShouldBe(StoreValue);
        request["stream"]!.GetValue<bool>().ShouldBe(StreamValue);
        request["temperature"]!.GetValue<double>().ShouldBe(TemperatureValue);
        request["top_logprobs"]!.GetValue<int>().ShouldBe(TopLogprobsValue);
        request["top_p"]!.GetValue<double>().ShouldBe(TopPValue);
        request["user"]!.GetValue<string>().ShouldBe(UserValue);
        request["max_tokens"]!.GetValue<int>().ShouldBe(MaxTokensValue);

        // Assert - Verify complex objects are set correctly
        request["compound_custom"].ShouldNotBeNull();
        request["documents"].ShouldNotBeNull();
        request["logit_bias"].ShouldNotBeNull();
        request["metadata"].ShouldNotBeNull();
        request["response_format"].ShouldNotBeNull();
        request["search_settings"].ShouldNotBeNull();
        request["stop"].ShouldNotBeNull();
        request["stream_options"].ShouldNotBeNull();
        request["tool_choice"].ShouldNotBeNull();
        request["tools"].ShouldNotBeNull();
        request["exclude_domains"].ShouldNotBeNull();
        request["function_call"].ShouldNotBeNull();
        request["functions"].ShouldNotBeNull();
        request["include_domains"].ShouldNotBeNull();
        request["messages"].ShouldNotBeNull();
    }

    [Fact]
    public void Builder_Should_Omit_Null_Optional_Parameters()
    {
        // Arrange & Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .Build();

        // Assert - Verify only required parameters exist
        request.ShouldContainKey("model");
        request.ShouldContainKey("messages");
        request.Count.ShouldBe(2); // Only model and messages should be present

        // Assert - Verify all optional parameters are omitted
        request.ShouldNotContainKey("citation_options");
        request.ShouldNotContainKey("compound_custom");
        request.ShouldNotContainKey("disable_tool_validation");
        request.ShouldNotContainKey("documents");
        request.ShouldNotContainKey("frequency_penalty");
        request.ShouldNotContainKey("include_reasoning");
        request.ShouldNotContainKey("logit_bias");
        request.ShouldNotContainKey("logprobs");
        request.ShouldNotContainKey("max_completion_tokens");
        request.ShouldNotContainKey("metadata");
        request.ShouldNotContainKey("n");
        request.ShouldNotContainKey("parallel_tool_calls");
        request.ShouldNotContainKey("presence_penalty");
        request.ShouldNotContainKey("reasoning_effort");
        request.ShouldNotContainKey("reasoning_format");
        request.ShouldNotContainKey("response_format");
        request.ShouldNotContainKey("search_settings");
        request.ShouldNotContainKey("seed");
        request.ShouldNotContainKey("service_tier");
        request.ShouldNotContainKey("stop");
        request.ShouldNotContainKey("store");
        request.ShouldNotContainKey("stream");
        request.ShouldNotContainKey("stream_options");
        request.ShouldNotContainKey("temperature");
        request.ShouldNotContainKey("tool_choice");
        request.ShouldNotContainKey("tools");
        request.ShouldNotContainKey("top_logprobs");
        request.ShouldNotContainKey("top_p");
        request.ShouldNotContainKey("user");

        // Assert - Verify deprecated parameters are also omitted
        request.ShouldNotContainKey("exclude_domains");
        request.ShouldNotContainKey("function_call");
        request.ShouldNotContainKey("functions");
        request.ShouldNotContainKey("include_domains");
        request.ShouldNotContainKey("max_tokens");
    }

    [Fact]
    public void Builder_Should_Handle_ResponseFormat_Parameter()
    {
        // Arrange
        const string responseFormat = """
                                      {
                                          "name": "math_response",
                                          "strict": true,
                                          "schema": {
                                              "type": "object",
                                              "properties": {
                                                  "answer": {"type": "number"}
                                              }
                                          }
                                      }
                                      """;

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithResponseFormat(responseFormat)
            .Build();

        // Assert
        request.ShouldContainKey("response_format");
        request["response_format"]!["type"]!.GetValue<string>().ShouldBe("json_schema");
        request["response_format"]!["json_schema"].ShouldNotBeNull();
    }

    [Fact]
    public void Builder_Should_Handle_Tools_Parameter()
    {
        // Arrange
        var tools = new JsonArray
        {
            new JsonObject
            {
                ["type"] = "function",
                ["function"] = new JsonObject
                {
                    ["name"] = "get_weather",
                    ["description"] = "Get weather information",
                    ["parameters"] = new JsonObject
                    {
                        ["type"] = "object",
                        ["properties"] = new JsonObject
                        {
                            ["location"] = new JsonObject { ["type"] = "string" }
                        }
                    }
                }
            }
        };

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages("What's the weather?")
            .WithTools(tools)
            .Build();

        // Assert
        request.ShouldContainKey("tools");
        var requestTools = request["tools"]!.AsArray();
        requestTools.Count.ShouldBe(1);
        requestTools[0]!["type"]!.GetValue<string>().ShouldBe("function");
    }

    [Fact]
    public void Builder_Should_Handle_Stop_Parameter_As_String()
    {
        // Arrange
        const string stopSequence = "\n";

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithStop(JsonValue.Create(stopSequence))
            .Build();

        // Assert
        request.ShouldContainKey("stop");
        request["stop"]!.GetValue<string>().ShouldBe(stopSequence);
    }

    [Fact]
    public void Builder_Should_Handle_Stop_Parameter_As_Array()
    {
        // Arrange
        var stopSequences = new JsonArray { "\n", "END", "STOP" };

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithStop(stopSequences)
            .Build();

        // Assert
        request.ShouldContainKey("stop");
        var stop = request["stop"]!.AsArray();
        stop.Count.ShouldBe(3);
    }

    [Fact]
    public void Builder_Should_Handle_All_Boolean_Parameters()
    {
        // Arrange & Act - Test all 6 boolean parameters in ChatCompletionRequestBuilder
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithDisableToolValidation(true)     // Boolean param 1: disable_tool_validation
            .WithIncludeReasoning(true)          // Boolean param 2: include_reasoning
            .WithLogprobs(true)                  // Boolean param 3: logprobs
            .WithParallelToolCalls(false)        // Boolean param 4: parallel_tool_calls
            .WithStore(true)                     // Boolean param 5: store
            .WithStream(true)                    // Boolean param 6: stream
            .Build();

        // Assert - Verify all 6 boolean parameters are set correctly
        request.ShouldContainKey("disable_tool_validation");
        request["disable_tool_validation"]!.GetValue<bool>().ShouldBeTrue();

        request.ShouldContainKey("include_reasoning");
        request["include_reasoning"]!.GetValue<bool>().ShouldBeTrue();

        request.ShouldContainKey("logprobs");
        request["logprobs"]!.GetValue<bool>().ShouldBeTrue();

        request.ShouldContainKey("parallel_tool_calls");
        request["parallel_tool_calls"]!.GetValue<bool>().ShouldBeFalse();

        request.ShouldContainKey("store");
        request["store"]!.GetValue<bool>().ShouldBeTrue();

        request.ShouldContainKey("stream");
        request["stream"]!.GetValue<bool>().ShouldBeTrue();
    }

    [Fact]
    public void Builder_Should_Handle_String_Parameters()
    {
        // Arrange & Act - Test all 6 string parameters in ChatCompletionRequestBuilder
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)                     // String param 1: model (required)
            .WithMessages(UserPrompt)
            .WithCitationOptions("enabled")              // String param 2: citation_options
            .WithReasoningEffort("medium")               // String param 3: reasoning_effort
            .WithReasoningFormat("parsed")               // String param 4: reasoning_format
            .WithServiceTier("on_demand")                // String param 5: service_tier
            .WithUser("user-123")                        // String param 6: user
            .Build();

        // Assert - Verify all 6 string parameters are set correctly
        request.ShouldContainKey("model");
        request["model"]!.GetValue<string>().ShouldBe(DefaultModel);

        request.ShouldContainKey("citation_options");
        request["citation_options"]!.GetValue<string>().ShouldBe("enabled");

        request.ShouldContainKey("reasoning_effort");
        request["reasoning_effort"]!.GetValue<string>().ShouldBe("medium");

        request.ShouldContainKey("reasoning_format");
        request["reasoning_format"]!.GetValue<string>().ShouldBe("parsed");

        request.ShouldContainKey("service_tier");
        request["service_tier"]!.GetValue<string>().ShouldBe("on_demand");

        request.ShouldContainKey("user");
        request["user"]!.GetValue<string>().ShouldBe("user-123");
    }

    [Fact]
    public void Builder_Should_Handle_Integer_Parameters()
    {
        // Arrange - Test all 5 integer parameters in ChatCompletionRequestBuilder
        const int maxCompletionTokens = 1000;
        const int maxTokens = 500;
        const int n = 3;
        const int seed = 42;
        const int topLogprobs = 5;

        // Act
#pragma warning disable CS0618 // Type or member is obsolete
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithMaxCompletionTokens(maxCompletionTokens)  // Integer param 1: max_completion_tokens
            .WithMaxTokens(maxTokens)                      // Integer param 2: max_tokens (deprecated)
            .WithN(n)                                      // Integer param 3: n
            .WithSeed(seed)                                // Integer param 4: seed
            .WithTopLogprobs(topLogprobs)                  // Integer param 5: top_logprobs
            .Build();
#pragma warning restore CS0618 // Type or member is obsolete

        // Assert - Verify all 5 integer parameters are set correctly
        request.ShouldContainKey("max_completion_tokens");
        request["max_completion_tokens"]!.GetValue<int>().ShouldBe(maxCompletionTokens);

        request.ShouldContainKey("max_tokens");
        request["max_tokens"]!.GetValue<int>().ShouldBe(maxTokens);

        request.ShouldContainKey("n");
        request["n"]!.GetValue<int>().ShouldBe(n);

        request.ShouldContainKey("seed");
        request["seed"]!.GetValue<int>().ShouldBe(seed);

        request.ShouldContainKey("top_logprobs");
        request["top_logprobs"]!.GetValue<int>().ShouldBe(topLogprobs);
    }

    [Fact]
    public void Builder_Should_Use_Static_Create_Method()
    {
        // Act - Create builder using static factory method
        var builder = ChatCompletionRequestBuilder.Create();

        // Assert - Verify Create() returns a new instance
        builder.ShouldNotBeNull();
        builder.ShouldBeOfType<ChatCompletionRequestBuilder>();

        // Assert - Verify each call to Create() returns a new instance
        var builder2 = ChatCompletionRequestBuilder.Create();
        builder2.ShouldNotBeNull();
        builder2.ShouldBeOfType<ChatCompletionRequestBuilder>();
        builder2.ShouldNotBeSameAs(builder); // Different instances

        // Assert - Verify the created builder can be used for method chaining
        var request = ChatCompletionRequestBuilder.Create()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithTemperature(TemperatureValue)
            .Build();

        // Assert - Verify the builder created via Create() produces valid requests
        request.ShouldNotBeNull();
        request.ShouldContainKey("model");
        request.ShouldContainKey("messages");
        request.ShouldContainKey("temperature");
        request["model"]!.GetValue<string>().ShouldBe(DefaultModel);
        request["temperature"]!.GetValue<double>().ShouldBe(TemperatureValue);
    }

    [Fact]
    public void Builder_Should_Handle_JsonObject_Parameters()
    {
        // Arrange - Test all 6 JsonObject parameters in ChatCompletionRequestBuilder
        var compoundCustom = new JsonObject { ["custom_field"] = "value" };
        var logitBias = new JsonObject { ["50256"] = -100 };
        var metadata = new JsonObject { ["request_id"] = "req-123" };
        const string responseFormatSchema = "{\"name\":\"response_schema\",\"strict\":true}";
        var searchSettings = new JsonObject { ["max_results"] = 10 };
        var streamOptions = new JsonObject { ["include_usage"] = true };

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithCompoundCustom(compoundCustom)       // JsonObject param 1: compound_custom
            .WithLogitBias(logitBias)                 // JsonObject param 2: logit_bias
            .WithMetadata(metadata)                   // JsonObject param 3: metadata
            .WithResponseFormat(responseFormatSchema) // JsonObject param 4: response_format (set via string)
            .WithSearchSettings(searchSettings)       // JsonObject param 5: search_settings
            .WithStreamOptions(streamOptions)         // JsonObject param 6: stream_options
            .Build();

        // Assert - Verify all 6 JsonObject parameters are set correctly
        request.ShouldContainKey("compound_custom");
        request["compound_custom"]!["custom_field"]!.GetValue<string>().ShouldBe("value");

        request.ShouldContainKey("logit_bias");
        request["logit_bias"]!["50256"]!.GetValue<int>().ShouldBe(-100);

        request.ShouldContainKey("metadata");
        request["metadata"]!["request_id"]!.GetValue<string>().ShouldBe("req-123");

        request.ShouldContainKey("response_format");
        request["response_format"]!["type"]!.GetValue<string>().ShouldBe("json_schema");
        request["response_format"]!["json_schema"].ShouldNotBeNull();

        request.ShouldContainKey("search_settings");
        request["search_settings"]!["max_results"]!.GetValue<int>().ShouldBe(10);

        request.ShouldContainKey("stream_options");
        request["stream_options"]!["include_usage"]!.GetValue<bool>().ShouldBeTrue();
    }

    [Fact]
    public void Builder_Should_Handle_Documents_Parameter()
    {
        // Arrange - Test documents parameter (JsonArray) with multiple documents
        var documents = new JsonArray
        {
            new JsonObject
            {
                ["content"] = "First document content",
                ["metadata"] = new JsonObject { ["source"] = "file1.txt", ["page"] = 1 }
            },
            new JsonObject
            {
                ["content"] = "Second document content",
                ["metadata"] = new JsonObject { ["source"] = "file2.txt", ["page"] = 2 }
            },
            new JsonObject
            {
                ["content"] = "Third document content",
                ["id"] = "doc-3",
                ["metadata"] = new JsonObject { ["source"] = "file3.txt", ["author"] = "John Doe" }
            }
        };

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages("Summarize the documents")
            .WithDocuments(documents)
            .Build();

        // Assert - Verify documents parameter is set correctly
        request.ShouldContainKey("documents");
        var requestDocs = request["documents"]!.AsArray();

        // Verify array has all 3 documents
        requestDocs.Count.ShouldBe(3);

        // Verify first document
        requestDocs[0]!["content"]!.GetValue<string>().ShouldBe("First document content");
        requestDocs[0]!["metadata"]!["source"]!.GetValue<string>().ShouldBe("file1.txt");
        requestDocs[0]!["metadata"]!["page"]!.GetValue<int>().ShouldBe(1);

        // Verify second document
        requestDocs[1]!["content"]!.GetValue<string>().ShouldBe("Second document content");
        requestDocs[1]!["metadata"]!["source"]!.GetValue<string>().ShouldBe("file2.txt");
        requestDocs[1]!["metadata"]!["page"]!.GetValue<int>().ShouldBe(2);

        // Verify third document
        requestDocs[2]!["content"]!.GetValue<string>().ShouldBe("Third document content");
        requestDocs[2]!["id"]!.GetValue<string>().ShouldBe("doc-3");
        requestDocs[2]!["metadata"]!["source"]!.GetValue<string>().ShouldBe("file3.txt");
        requestDocs[2]!["metadata"]!["author"]!.GetValue<string>().ShouldBe("John Doe");
    }

    [Fact]
    public void Builder_Should_Handle_All_Double_Parameters()
    {
        // Arrange - Test all 4 double parameters in ChatCompletionRequestBuilder
        const double temperature = 0.8;
        const double topP = 0.95;
        const double frequencyPenalty = 0.6;
        const double presencePenalty = 0.4;

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithTemperature(temperature)           // Double param 1: temperature
            .WithTopP(topP)                         // Double param 2: top_p
            .WithFrequencyPenalty(frequencyPenalty) // Double param 3: frequency_penalty
            .WithPresencePenalty(presencePenalty)   // Double param 4: presence_penalty
            .Build();

        // Assert - Verify all 4 double parameters are set correctly
        request.ShouldContainKey("temperature");
        request["temperature"]!.GetValue<double>().ShouldBe(temperature);

        request.ShouldContainKey("top_p");
        request["top_p"]!.GetValue<double>().ShouldBe(topP);

        request.ShouldContainKey("frequency_penalty");
        request["frequency_penalty"]!.GetValue<double>().ShouldBe(frequencyPenalty);

        request.ShouldContainKey("presence_penalty");
        request["presence_penalty"]!.GetValue<double>().ShouldBe(presencePenalty);
    }

    [Fact]
    public void Builder_Should_Handle_All_JsonArray_Parameters()
    {
        // Arrange - Test all JsonArray parameters (documents, tools, excludeDomains, functions, includeDomains)
        var documents = new JsonArray { new JsonObject { ["content"] = "doc1" } };
        var tools = new JsonArray
        {
            new JsonObject
            {
                ["type"] = "function",
                ["function"] = new JsonObject { ["name"] = "test_tool" }
            }
        };
        var excludeDomains = new JsonArray { "spam.com" };
        var functions = new JsonArray { new JsonObject { ["name"] = "test_func" } };
        var includeDomains = new JsonArray { "trusted.com" };

        // Act
#pragma warning disable CS0618 // Type or member is obsolete
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithDocuments(documents)           // JsonArray param 1: documents
            .WithTools(tools)                   // JsonArray param 2: tools
            .WithExcludeDomains(excludeDomains) // JsonArray param 3: exclude_domains (deprecated)
            .WithFunctions(functions)           // JsonArray param 4: functions (deprecated)
            .WithIncludeDomains(includeDomains) // JsonArray param 5: include_domains (deprecated)
            .Build();
#pragma warning restore CS0618 // Type or member is obsolete

        // Assert - Verify all 5 JsonArray parameters are set correctly
        request.ShouldContainKey("documents");
        request["documents"]!.AsArray().Count.ShouldBe(1);

        request.ShouldContainKey("tools");
        request["tools"]!.AsArray().Count.ShouldBe(1);

        request.ShouldContainKey("exclude_domains");
        request["exclude_domains"]!.AsArray().Count.ShouldBe(1);

        request.ShouldContainKey("functions");
        request["functions"]!.AsArray().Count.ShouldBe(1);

        request.ShouldContainKey("include_domains");
        request["include_domains"]!.AsArray().Count.ShouldBe(1);
    }

    [Fact]
    public void Builder_Should_Handle_All_JsonNode_Parameters()
    {
        // Arrange - Test all JsonNode parameters (stop, toolChoice, functionCall)
        var stopNode = JsonNode.Parse("\"STOP\"");
        var toolChoiceNode = JsonNode.Parse("\"auto\"");
        var functionCallNode = JsonNode.Parse("\"none\"");

        // Act
#pragma warning disable CS0618 // Type or member is obsolete
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithStop(stopNode!)              // JsonNode param 1: stop
            .WithToolChoice(toolChoiceNode!)  // JsonNode param 2: tool_choice
            .WithFunctionCall(functionCallNode!) // JsonNode param 3: function_call (deprecated)
            .Build();
#pragma warning restore CS0618 // Type or member is obsolete

        // Assert - Verify all 3 JsonNode parameters are set correctly
        request.ShouldContainKey("stop");
        request["stop"]!.GetValue<string>().ShouldBe("STOP");

        request.ShouldContainKey("tool_choice");
        request["tool_choice"]!.GetValue<string>().ShouldBe("auto");

        request.ShouldContainKey("function_call");
        request["function_call"]!.GetValue<string>().ShouldBe("none");
    }

    [Fact]
    public void Builder_Should_Overwrite_Messages_On_Multiple_Calls()
    {
        // Arrange & Act - Call WithMessages twice, second call should overwrite
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages("First message", "First system")
            .WithMessages("Second message", "Second system") // This should overwrite
            .Build();

        // Assert - Only the second set of messages should be present
        var messages = request["messages"]!.AsArray();
        messages.Count.ShouldBe(2);
        messages[0]!["role"]!.GetValue<string>().ShouldBe("system");
        messages[0]!["content"]!.GetValue<string>().ShouldBe("Second system");
        messages[1]!["role"]!.GetValue<string>().ShouldBe("user");
        messages[1]!["content"]!.GetValue<string>().ShouldBe("Second message");
    }

    [Fact]
    public void Builder_Should_Handle_Boundary_Values_For_Integer_Parameters()
    {
        // Arrange - Test boundary values
        const int zero = 0;
        const int negativeValue = -1;
        const int largeValue = 1000000;

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithN(zero)
            .WithSeed(negativeValue)
            .WithMaxCompletionTokens(largeValue)
            .Build();

        // Assert
        request["n"]!.GetValue<int>().ShouldBe(zero);
        request["seed"]!.GetValue<int>().ShouldBe(negativeValue);
        request["max_completion_tokens"]!.GetValue<int>().ShouldBe(largeValue);
    }

    [Fact]
    public void Builder_Should_Handle_Boundary_Values_For_Double_Parameters()
    {
        // Arrange - Test boundary values for doubles
        const double minTemperature = 0.0;
        const double maxTemperature = 2.0;
        const double minPenalty = -2.0;
        const double maxPenalty = 2.0;

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithTemperature(minTemperature)
            .WithTopP(maxTemperature)
            .WithFrequencyPenalty(minPenalty)
            .WithPresencePenalty(maxPenalty)
            .Build();

        // Assert
        request["temperature"]!.GetValue<double>().ShouldBe(minTemperature);
        request["top_p"]!.GetValue<double>().ShouldBe(maxTemperature);
        request["frequency_penalty"]!.GetValue<double>().ShouldBe(minPenalty);
        request["presence_penalty"]!.GetValue<double>().ShouldBe(maxPenalty);
    }

    [Fact]
    public void Builder_Should_Handle_Empty_JsonArray_Parameters()
    {
        // Arrange - Test empty arrays
        var emptyDocuments = new JsonArray();
        var emptyTools = new JsonArray();

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithDocuments(emptyDocuments)
            .WithTools(emptyTools)
            .Build();

        // Assert - Empty arrays should still be included
        request.ShouldContainKey("documents");
        request["documents"]!.AsArray().Count.ShouldBe(0);

        request.ShouldContainKey("tools");
        request["tools"]!.AsArray().Count.ShouldBe(0);
    }

    [Fact]
    public void Builder_Should_Handle_Empty_JsonObject_Parameters()
    {
        // Arrange - Test empty objects
        var emptyMetadata = new JsonObject();
        var emptyLogitBias = new JsonObject();

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithMetadata(emptyMetadata)
            .WithLogitBias(emptyLogitBias)
            .Build();

        // Assert - Empty objects should still be included
        request.ShouldContainKey("metadata");
        request["metadata"]!.AsObject().Count.ShouldBe(0);

        request.ShouldContainKey("logit_bias");
        request["logit_bias"]!.AsObject().Count.ShouldBe(0);
    }

    [Fact]
    public void Builder_Should_Handle_ToolChoice_As_Object()
    {
        // Arrange - Test toolChoice as an object (not just string)
        var toolChoiceObject = new JsonObject
        {
            ["type"] = "function",
            ["function"] = new JsonObject { ["name"] = "specific_function" }
        };

        // Act
        var request = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt)
            .WithToolChoice(toolChoiceObject)
            .Build();

        // Assert
        request.ShouldContainKey("tool_choice");
        request["tool_choice"]!["type"]!.GetValue<string>().ShouldBe("function");
        request["tool_choice"]!["function"]!["name"]!.GetValue<string>().ShouldBe("specific_function");
    }

    [Fact]
    public void Builder_Should_Throw_On_Invalid_ResponseFormat_Json()
    {
        // Arrange
        const string invalidJson = "{ invalid json }";

        // Act & Assert
        var builder = new ChatCompletionRequestBuilder()
            .WithModel(DefaultModel)
            .WithMessages(UserPrompt);

        Should.Throw<JsonException>(() => builder.WithResponseFormat(invalidJson));
    }
}
