// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace GroqApiLibrary.Models;

public static class AgentModels
{
    public static readonly Model GROQ_COMPOUND_MINI = new Model
    {
        Id = "groq/compound-mini",
        Object = "model",
        Created = 1756949707,
        OwnedBy = "Groq",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 8192
    };

    public static readonly Model GROQ_COMPOUND = new Model
    {
        Id = "groq/compound",
        Object = "model",
        Created = 1756949530,
        OwnedBy = "Groq",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 8192
    };
}
