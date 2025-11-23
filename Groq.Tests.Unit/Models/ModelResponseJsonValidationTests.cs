namespace Groq.Tests.Unit.Models;

/// <summary>
///     Tests for JSON serialization and deserialization of Model and ModelListResponse classes.
///     Ensures proper mapping between JSON responses from the Groq API and C# objects.
/// </summary>
public class ModelResponseJsonValidationTests
{
    #region Model Class Tests

    [Fact]
    public void Model_Should_Deserialize_From_Valid_Json()
    {
        // Arrange
        const string json = """
            {
                "id": "llama-3.1-8b-instant",
                "object": "model",
                "created": 1693721698,
                "owned_by": "Meta",
                "active": true,
                "context_window": 131072,
                "public_apps": null,
                "max_completion_tokens": 8192
            }
            """;

        // Act
        var model = JsonSerializer.Deserialize<Model>(json);

        // Assert
        model.ShouldNotBeNull();
        model.Id.ShouldBe("llama-3.1-8b-instant");
        model.Object.ShouldBe("model");
        model.Created.ShouldBe(1693721698);
        model.OwnedBy.ShouldBe("Meta");
        model.Active.ShouldBeTrue();
        model.ContextWindow.ShouldBe(131072);
        model.PublicApps.ShouldBeNull();
        model.MaxCompletionTokens.ShouldBe(8192);
    }

    [Fact]
    public void Model_Should_Deserialize_With_PublicApps_Object()
    {
        // Arrange
        const string json = """
            {
                "id": "whisper-large-v3",
                "object": "model",
                "created": 1677649963,
                "owned_by": "OpenAI",
                "active": true,
                "context_window": 448,
                "public_apps": {
                    "app_count": 5,
                    "featured": true
                },
                "max_completion_tokens": 448
            }
            """;

        // Act
        var model = JsonSerializer.Deserialize<Model>(json);

        // Assert
        model.ShouldNotBeNull();
        model.Id.ShouldBe("whisper-large-v3");
        model.PublicApps.ShouldNotBeNull();
    }

    [Fact]
    public void Model_Should_Serialize_To_Valid_Json()
    {
        // Arrange
        var model = new Model
        {
            Id = "llama-3.3-70b-versatile",
            Object = "model",
            Created = 1730419200,
            OwnedBy = "Meta",
            Active = true,
            ContextWindow = 131072,
            PublicApps = null,
            MaxCompletionTokens = 32768
        };

        // Act
        var json = JsonSerializer.Serialize(model);
        var deserializedModel = JsonSerializer.Deserialize<Model>(json);

        // Assert
        deserializedModel.ShouldNotBeNull();
        deserializedModel.Id.ShouldBe(model.Id);
        deserializedModel.Object.ShouldBe(model.Object);
        deserializedModel.Created.ShouldBe(model.Created);
        deserializedModel.OwnedBy.ShouldBe(model.OwnedBy);
        deserializedModel.Active.ShouldBe(model.Active);
        deserializedModel.ContextWindow.ShouldBe(model.ContextWindow);
        deserializedModel.PublicApps.ShouldBeNull();
        deserializedModel.MaxCompletionTokens.ShouldBe(model.MaxCompletionTokens);
    }

    [Fact]
    public void Model_Should_Handle_Round_Trip_Serialization()
    {
        // Arrange - Builder a model, serialize it, deserialize it, and compare
        var originalModel = new Model
        {
            Id = "llama-guard-3-8b",
            Object = "model",
            Created = 1720094400,
            OwnedBy = "Meta",
            Active = true,
            ContextWindow = 8192,
            PublicApps = null,
            MaxCompletionTokens = 8192
        };

        // Act - Round trip: Object → JSON → Object
        var json = JsonSerializer.Serialize(originalModel);
        var deserializedModel = JsonSerializer.Deserialize<Model>(json);
        var jsonAgain = JsonSerializer.Serialize(deserializedModel);

        // Assert - Both JSON strings should be identical
        json.ShouldBe(jsonAgain);
        deserializedModel.ShouldNotBeNull();
        deserializedModel.Id.ShouldBe(originalModel.Id);
        deserializedModel.Active.ShouldBe(originalModel.Active);
    }

    [Fact]
    public void Model_Should_Handle_Large_Context_Window_Values()
    {
        // Arrange - Test with very large context window (e.g., 1M tokens)
        const string json = """
            {
                "id": "future-model-1m-context",
                "object": "model",
                "created": 1735689600,
                "owned_by": "Groq",
                "active": true,
                "context_window": 1000000,
                "public_apps": null,
                "max_completion_tokens": 100000
            }
            """;

        // Act
        var model = JsonSerializer.Deserialize<Model>(json);

        // Assert
        model.ShouldNotBeNull();
        model.ContextWindow.ShouldBe(1_000_000);
        model.MaxCompletionTokens.ShouldBe(100_000);
    }

    [Fact]
    public void Model_Should_Handle_Inactive_Model()
    {
        // Arrange
        const string json = """
            {
                "id": "deprecated-model",
                "object": "model",
                "created": 1640995200,
                "owned_by": "Legacy",
                "active": false,
                "context_window": 2048,
                "public_apps": null,
                "max_completion_tokens": 1024
            }
            """;

        // Act
        var model = JsonSerializer.Deserialize<Model>(json);

        // Assert
        model.ShouldNotBeNull();
        model.Active.ShouldBeFalse();
    }

    [Fact]
    public void Model_Should_Preserve_Json_Property_Names()
    {
        // Arrange
        var model = new Model
        {
            Id = "test-model",
            Object = "model",
            Created = 1234567890,
            OwnedBy = "Test Owner",
            Active = true,
            ContextWindow = 4096,
            PublicApps = null,
            MaxCompletionTokens = 2048
        };

        // Act
        var json = JsonSerializer.Serialize(model);

        // Assert - Verify JSON property names use snake_case
        json.ShouldContain("\"id\":");
        json.ShouldContain("\"object\":");
        json.ShouldContain("\"created\":");
        json.ShouldContain("\"owned_by\":");
        json.ShouldContain("\"active\":");
        json.ShouldContain("\"context_window\":");
        json.ShouldContain("\"public_apps\":");
        json.ShouldContain("\"max_completion_tokens\":");
    }

    #endregion

    #region ModelListResponse Class Tests

    [Fact]
    public void ModelListResponse_Should_Deserialize_From_Valid_Json()
    {
        // Arrange
        const string json = """
            {
                "object": "list",
                "data": [
                    {
                        "id": "llama-3.1-8b-instant",
                        "object": "model",
                        "created": 1693721698,
                        "owned_by": "Meta",
                        "active": true,
                        "context_window": 131072,
                        "public_apps": null,
                        "max_completion_tokens": 8192
                    },
                    {
                        "id": "whisper-large-v3",
                        "object": "model",
                        "created": 1677649963,
                        "owned_by": "OpenAI",
                        "active": true,
                        "context_window": 448,
                        "public_apps": null,
                        "max_completion_tokens": 448
                    }
                ]
            }
            """;

        // Act
        var response = JsonSerializer.Deserialize<ModelListResponse>(json);

        // Assert
        response.ShouldNotBeNull();
        response.Object.ShouldBe("list");
        response.Data.ShouldNotBeNull();
        response.Data.Count.ShouldBe(2);

        response.Data[0].Id.ShouldBe("llama-3.1-8b-instant");
        response.Data[0].OwnedBy.ShouldBe("Meta");

        response.Data[1].Id.ShouldBe("whisper-large-v3");
        response.Data[1].OwnedBy.ShouldBe("OpenAI");
    }

    [Fact]
    public void ModelListResponse_Should_Handle_Empty_Data_Array()
    {
        // Arrange
        const string json = """
            {
                "object": "list",
                "data": []
            }
            """;

        // Act
        var response = JsonSerializer.Deserialize<ModelListResponse>(json);

        // Assert
        response.ShouldNotBeNull();
        response.Object.ShouldBe("list");
        response.Data.ShouldNotBeNull();
        response.Data.Count.ShouldBe(0);
    }

    [Fact]
    public void ModelListResponse_Should_Serialize_To_Valid_Json()
    {
        // Arrange
        var response = new ModelListResponse
        {
            Object = "list",
            Data =
            [
                new Model
                {
                    Id = "model-1",
                    Object = "model",
                    Created = 1234567890,
                    OwnedBy = "Owner1",
                    Active = true,
                    ContextWindow = 4096,
                    PublicApps = null,
                    MaxCompletionTokens = 2048
                },
                new Model
                {
                    Id = "model-2",
                    Object = "model",
                    Created = 1234567891,
                    OwnedBy = "Owner2",
                    Active = false,
                    ContextWindow = 8192,
                    PublicApps = null,
                    MaxCompletionTokens = 4096
                }
            ]
        };

        // Act
        var json = JsonSerializer.Serialize(response);
        var deserializedResponse = JsonSerializer.Deserialize<ModelListResponse>(json);

        // Assert
        deserializedResponse.ShouldNotBeNull();
        deserializedResponse.Object.ShouldBe("list");
        deserializedResponse.Data.Count.ShouldBe(2);
        deserializedResponse.Data[0].Id.ShouldBe("model-1");
        deserializedResponse.Data[1].Id.ShouldBe("model-2");
    }

    [Fact]
    public void ModelListResponse_Should_Handle_Round_Trip_Serialization()
    {
        // Arrange
        var originalResponse = new ModelListResponse
        {
            Object = "list",
            Data =
            [
                new Model
                {
                    Id = "llama-3.3-70b-versatile",
                    Object = "model",
                    Created = 1730419200,
                    OwnedBy = "Meta",
                    Active = true,
                    ContextWindow = 131072,
                    PublicApps = null,
                    MaxCompletionTokens = 32768
                }
            ]
        };

        // Act - Round trip: Object → JSON → Object → JSON
        var json1 = JsonSerializer.Serialize(originalResponse);
        var deserializedResponse = JsonSerializer.Deserialize<ModelListResponse>(json1);
        var json2 = JsonSerializer.Serialize(deserializedResponse);

        // Assert
        json1.ShouldBe(json2);
        deserializedResponse.ShouldNotBeNull();
        deserializedResponse.Data.Count.ShouldBe(1);
        deserializedResponse.Data[0].Id.ShouldBe("llama-3.3-70b-versatile");
    }

    [Fact]
    public void ModelListResponse_Should_Handle_Multiple_Models_From_Different_Owners()
    {
        // Arrange
        const string json = """
            {
                "object": "list",
                "data": [
                    {
                        "id": "llama-3.1-8b-instant",
                        "object": "model",
                        "created": 1693721698,
                        "owned_by": "Meta",
                        "active": true,
                        "context_window": 131072,
                        "public_apps": null,
                        "max_completion_tokens": 8192
                    },
                    {
                        "id": "whisper-large-v3",
                        "object": "model",
                        "created": 1677649963,
                        "owned_by": "OpenAI",
                        "active": true,
                        "context_window": 448,
                        "public_apps": null,
                        "max_completion_tokens": 448
                    },
                    {
                        "id": "qwen-2.5-32b-instruct",
                        "object": "model",
                        "created": 1725580800,
                        "owned_by": "Alibaba Cloud",
                        "active": true,
                        "context_window": 32768,
                        "public_apps": null,
                        "max_completion_tokens": 8192
                    }
                ]
            }
            """;

        // Act
        var response = JsonSerializer.Deserialize<ModelListResponse>(json);

        // Assert
        response.ShouldNotBeNull();
        response.Data.Count.ShouldBe(3);
        response.Data[0].OwnedBy.ShouldBe("Meta");
        response.Data[1].OwnedBy.ShouldBe("OpenAI");
        response.Data[2].OwnedBy.ShouldBe("Alibaba Cloud");
    }

    [Fact]
    public void ModelListResponse_Should_Preserve_Json_Property_Names()
    {
        // Arrange
        var response = new ModelListResponse
        {
            Object = "list",
            Data =
            [
                new Model
                {
                    Id = "test-model",
                    Object = "model",
                    Created = 1234567890,
                    OwnedBy = "Test",
                    Active = true,
                    ContextWindow = 1024,
                    PublicApps = null,
                    MaxCompletionTokens = 512
                }
            ]
        };

        // Act
        var json = JsonSerializer.Serialize(response);

        // Assert - Verify JSON property names
        json.ShouldContain("\"object\":");
        json.ShouldContain("\"data\":");
        json.ShouldContain("\"owned_by\":");
        json.ShouldContain("\"context_window\":");
        json.ShouldContain("\"max_completion_tokens\":");
    }

    #endregion

    #region Edge Cases and Error Handling

    [Fact]
    public void Model_Should_Throw_When_Required_Properties_Missing()
    {
        // Arrange - Missing required "id" property
        const string json = """
            {
                "object": "model",
                "created": 1693721698,
                "owned_by": "Meta",
                "active": true,
                "context_window": 131072,
                "public_apps": null,
                "max_completion_tokens": 8192
            }
            """;

        // Act & Assert
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<Model>(json));
    }

    [Fact]
    public void ModelListResponse_Should_Throw_When_Required_Properties_Missing()
    {
        // Arrange - Missing required "object" property
        const string json = """
            {
                "data": []
            }
            """;

        // Act & Assert
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<ModelListResponse>(json));
    }

    [Fact]
    public void Model_Should_Handle_Malformed_Json_Gracefully()
    {
        // Arrange
        const string malformedJson = "{ invalid json }";

        // Act & Assert
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<Model>(malformedJson));
    }

    [Fact]
    public void ModelListResponse_Should_Handle_Malformed_Json_Gracefully()
    {
        // Arrange
        const string malformedJson = "{ \"object\": \"list\", \"data\": [ invalid ] }";

        // Act & Assert
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<ModelListResponse>(malformedJson));
    }

    #endregion
}
