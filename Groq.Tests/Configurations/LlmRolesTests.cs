using Groq.Core.Configurations;

namespace Groq.Tests.Configurations;

/// <summary>
/// Unit tests for the LlmRoles static configuration class.
/// </summary>
public class LlmRolesTests
{
    [Fact]
    public void LlmRoles_SystemRole_IsCorrect()
    {
        // Assert
        Assert.Equal("system", LlmRoles.SystemRole);
    }

    [Fact]
    public void LlmRoles_UserRole_IsCorrect()
    {
        // Assert
        Assert.Equal("user", LlmRoles.UserRole);
    }

    [Fact]
    public void LlmRoles_AssistantRole_IsCorrect()
    {
        // Assert
        Assert.Equal("assistant", LlmRoles.AssistantRole);
    }

    [Fact]
    public void LlmRoles_ToolRole_IsCorrect()
    {
        // Assert
        Assert.Equal("tool", LlmRoles.ToolRole);
    }

    [Fact]
    public void LlmRoles_AllRoles_AreNotNull()
    {
        // Assert
        Assert.NotNull(LlmRoles.SystemRole);
        Assert.NotNull(LlmRoles.UserRole);
        Assert.NotNull(LlmRoles.AssistantRole);
        Assert.NotNull(LlmRoles.ToolRole);
    }

    [Fact]
    public void LlmRoles_AllRoles_AreNotEmpty()
    {
        // Assert
        Assert.NotEmpty(LlmRoles.SystemRole);
        Assert.NotEmpty(LlmRoles.UserRole);
        Assert.NotEmpty(LlmRoles.AssistantRole);
        Assert.NotEmpty(LlmRoles.ToolRole);
    }

    [Fact]
    public void LlmRoles_AllRoles_AreLowercase()
    {
        // Assert
        Assert.Equal(LlmRoles.SystemRole.ToLowerInvariant(), LlmRoles.SystemRole);
        Assert.Equal(LlmRoles.UserRole.ToLowerInvariant(), LlmRoles.UserRole);
        Assert.Equal(LlmRoles.AssistantRole.ToLowerInvariant(), LlmRoles.AssistantRole);
        Assert.Equal(LlmRoles.ToolRole.ToLowerInvariant(), LlmRoles.ToolRole);
    }

    [Fact]
    public void LlmRoles_AllRoles_AreUnique()
    {
        // Arrange
        var roles = new[] 
        { 
            LlmRoles.SystemRole, 
            LlmRoles.UserRole, 
            LlmRoles.AssistantRole, 
            LlmRoles.ToolRole 
        };

        // Assert
        Assert.Equal(4, roles.Distinct().Count());
    }
}