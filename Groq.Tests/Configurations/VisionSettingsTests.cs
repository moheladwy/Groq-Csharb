using Groq.Core.Configurations;

namespace Groq.Tests.Configurations;

/// <summary>
/// Unit tests for the VisionSettings static configuration class.
/// </summary>
public class VisionSettingsTests
{
    [Fact]
    public void VisionSettings_DefaultVisionModel_IsCorrect()
    {
        // Assert
        Assert.Equal("meta-llama/llama-4-scout-17b-16e-instruct", VisionSettings.DefaultVisionModel);
    }

    [Fact]
    public void VisionSettings_MaxImageSizeMb_IsCorrect()
    {
        // Assert
        Assert.Equal(20, VisionSettings.MaxImageSizeMb);
    }

    [Fact]
    public void VisionSettings_MaxBase64SizeMb_IsCorrect()
    {
        // Assert
        Assert.Equal(4, VisionSettings.MaxBase64SizeMb);
    }

    [Fact]
    public void VisionSettings_MaxImageResolutionLimitMPixels_IsCorrect()
    {
        // Assert
        Assert.Equal(33, VisionSettings.MaxImageResolutionLimitMPixels);
    }

    [Fact]
    public void VisionSettings_AllVisionModels_ContainsDefaultModel()
    {
        // Assert
        Assert.Contains(VisionSettings.DefaultVisionModel, VisionSettings.AllVisionModels);
    }

    [Fact]
    public void VisionSettings_AllVisionModels_IsNotEmpty()
    {
        // Assert
        Assert.NotEmpty(VisionSettings.AllVisionModels);
    }

    [Fact]
    public void VisionSettings_MaxImageSizeMb_IsGreaterThanBase64Size()
    {
        // Assert - URL-based images can be larger than base64 encoded
        Assert.True(VisionSettings.MaxImageSizeMb > VisionSettings.MaxBase64SizeMb);
    }

    [Fact]
    public void VisionSettings_AllSizeLimits_ArePositive()
    {
        // Assert
        Assert.True(VisionSettings.MaxImageSizeMb > 0);
        Assert.True(VisionSettings.MaxBase64SizeMb > 0);
        Assert.True(VisionSettings.MaxImageResolutionLimitMPixels > 0);
    }
}