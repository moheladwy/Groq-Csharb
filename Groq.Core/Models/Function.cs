using System.Text.Json.Nodes;

namespace Groq.Core.Models;

/// <summary>
/// Represents a function that can be called by the model during execution.
/// Functions define the interface, parameters, and execution logic for tools that extend model capabilities.
/// </summary>
public class Function
{
    /// <summary>
    /// Gets or sets the name of the function.
    /// </summary>
    /// <value>The function name that will be used when the model calls this function.</value>
    /// <remarks>Should be descriptive and follow standard naming conventions (e.g., "get_weather", "search_database").</remarks>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of what the function does.
    /// </summary>
    /// <value>A clear description that helps the model understand when and how to use this function.</value>
    /// <remarks>Provide detailed information about the function's purpose, expected inputs, and outputs to improve model decision-making.</remarks>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the JSON schema describing the function's parameters.
    /// </summary>
    /// <value>A JSON object defining the parameters structure, types, and validation rules.</value>
    /// <remarks>
    /// Should follow JSON Schema format with properties like:
    /// - type: The schema type (typically "object")
    /// - properties: Dictionary of parameter definitions with their types and descriptions
    /// - required: Array of required parameter names
    /// </remarks>
    public JsonObject Parameters { get; set; }

    /// <summary>
    /// Gets or sets the asynchronous execution handler for this function.
    /// </summary>
    /// <value>An async function that takes a JSON string of arguments and returns a JSON string result.</value>
    /// <remarks>
    /// The input string contains the function arguments as JSON, and the function should return its result as a JSON string.
    /// Handle exceptions appropriately and return error information in the result when necessary.
    /// </remarks>
    public Func<string, Task<string>> ExecuteAsync { get; set; }
}
