namespace GroqApiLibrary.Models;

public class Tool
{
    public string Type { get; set; } = "function";
    public Function Function { get; set; }
}
