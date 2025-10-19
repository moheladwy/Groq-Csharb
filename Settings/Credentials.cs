namespace GroqApiLibrary.Settings;

public class Credentials
{
  public const string Section = "Groq";
  public required string ApiKey { get; set; }
  public required string Model { get; set; }
}
