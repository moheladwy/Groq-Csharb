using Groq.Core.Models;

namespace Groq.Core.Settings;

/// <summary>
///     Contains settings for the Groq API vision features.
/// </summary>
public class VisionSettings
{
    /// <summary>Default vision model name.</summary>
    public const string DefaultVisionModel = "meta-llama/llama-4-scout-17b-16e-instruct";

    /// <summary>
    ///     The maximum allowed size for a request containing an image URL as input is 20MB.
    ///     Requests larger than this limit will return a 400 error.
    /// </summary>
    public const int MaxImageSizeMb = 20;

    /// <summary>
    ///     The maximum allowed size for a request containing a base64 encoded image is 4MB.
    ///     Requests larger than this limit will return a 413 error.
    /// </summary>
    public const int MaxBase64SizeMb = 4;

    /// <summary>
    ///     The maximum allowed resolution for a request containing images is 33 megapixels (33177600 total pixels) per image.
    ///     Images larger than this limit will return a 400 error.
    /// </summary>
    public const int MaxImageResolutionLimitMPixels = 33;

    /// <summary>Comma-separated list of supported vision model names.</summary>
    public static string AllVisionModels =>
        $"{VisionModels.LLAMA_4_SCOUT_17B_16E_INSTRUCT.Id},{VisionModels.LLAMA_4_MAVERICK_17B_128E_INSTRUCT.Id}";
}
