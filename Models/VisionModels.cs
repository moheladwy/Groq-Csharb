// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace GroqApiLibrary.Models;

public static class VisionModels
{
    public static readonly Model LLAMA_4_SCOUT_17B_16E_INSTRUCT = new Model
    {
        Id = "meta-llama/llama-4-scout-17b-16e-instruct",
        Object = "model",
        Created = 1743874824,
        OwnedBy = "Meta",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 8192
    };

    public static readonly Model LLAMA_4_MAVERICK_17B_128E_INSTRUCT = new Model
    {
        Id = "meta-llama/llama-4-maverick-17b-128e-instruct",
        Object = "model",
        Created = 1743877158,
        OwnedBy = "Meta",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 8192
    };
}
