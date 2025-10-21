// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace GroqApiLibrary.Models;

public static class AudioModels
{
    public static readonly Model WHISPER_LARGE_V3_TURBO = new Model
    {
        Id = "whisper-large-v3-turbo",
        Object = "model",
        Created = 1728413088,
        OwnedBy = "OpenAI",
        Active = true,
        ContextWindow = 448,
        PublicApps = null,
        MaxCompletionTokens = 448
    };

    public static readonly Model WHISPER_LARGE_V3 = new Model
    {
        Id = "whisper-large-v3",
        Object = "model",
        Created = 1693721698,
        OwnedBy = "OpenAI",
        Active = true,
        ContextWindow = 448,
        PublicApps = null,
        MaxCompletionTokens = 448
    };

    public static readonly Model PLAYAI_TTS = new Model
    {
        Id = "playai-tts",
        Object = "model",
        Created = 1740682771,
        OwnedBy = "PlayAI",
        Active = true,
        ContextWindow = 8192,
        PublicApps = null,
        MaxCompletionTokens = 8192
    };

    public static readonly Model PLAYAI_TTS_ARABIC = new Model
    {
        Id = "playai-tts-arabic",
        Object = "model",
        Created = 1740682783,
        OwnedBy = "PlayAI",
        Active = true,
        ContextWindow = 8192,
        PublicApps = null,
        MaxCompletionTokens = 8192
    };
}
