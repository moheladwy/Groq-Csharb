using System.Text.Json.Nodes;

namespace GroqApiLibrary.Models;

public class Function
{
  public string Name { get; set; }
  public string Description { get; set; }
  public JsonObject Parameters { get; set; }
  public Func<string, Task<string>> ExecuteAsync { get; set; }
}
