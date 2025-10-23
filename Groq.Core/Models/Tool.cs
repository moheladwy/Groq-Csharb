namespace Groq.Core.Models;

/// <summary>
///     Represents a tool that can be called by the model during execution.
///     Tools enable models to perform specific actions or access external functionality through function calling.
/// </summary>
public class Tool
{
    /// <summary>
    ///     Gets or sets the type of tool. Currently only "function" is supported.
    /// </summary>
    /// <value>The tool type. Defaults to "function".</value>
    public string Type { get; set; } = "function";

    /// <summary>
    ///     Gets or sets the function definition that describes the tool's behavior and parameters.
    /// </summary>
    /// <value>
    ///     A <see cref="Models.Function" /> object containing the function's name, description, parameters, and execution
    ///     logic.
    /// </value>
    public required Function Function { get; set; }
}
