// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace GroqApiLibrary.Models;

public static class ChatModels
{
    public static readonly Model OPENAI_GPT_OSS_120B = new Model
    {
        Id = "openai/gpt-oss-120b",
        Object = "model",
        Created = 1754408224,
        OwnedBy = "OpenAI",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 65536
    };

    public static readonly Model OPENAI_GPT_OSS_20B = new Model
    {
        Id = "openai/gpt-oss-20b",
        Object = "model",
        Created = 1754407957,
        OwnedBy = "OpenAI",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 65536
    };

    public static readonly Model LLAMA_PROMPT_GUARD_2_86M = new Model
    {
        Id = "meta-llama/llama-prompt-guard-2-86m",
        Object = "model",
        Created = 1748632165,
        OwnedBy = "Meta",
        Active = true,
        ContextWindow = 512,
        PublicApps = null,
        MaxCompletionTokens = 512
    };

    public static readonly Model QWEN3_32B = new Model
    {
        Id = "qwen/qwen3-32b",
        Object = "model",
        Created = 1748396646,
        OwnedBy = "Alibaba Cloud",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 40960
    };

    public static readonly Model LLAMA_3_1_8B_INSTANT = new Model
    {
        Id = "llama-3.1-8b-instant",
        Object = "model",
        Created = 1693721698,
        OwnedBy = "Meta",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 131072
    };

    public static readonly Model LLAMA_GUARD_4_12B = new Model
    {
        Id = "meta-llama/llama-guard-4-12b",
        Object = "model",
        Created = 1746743847,
        OwnedBy = "Meta",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 1024
    };

    public static readonly Model LLAMA_PROMPT_GUARD_2_22M = new Model
    {
        Id = "meta-llama/llama-prompt-guard-2-22m",
        Object = "model",
        Created = 1748632101,
        OwnedBy = "Meta",
        Active = true,
        ContextWindow = 512,
        PublicApps = null,
        MaxCompletionTokens = 512
    };

    public static readonly Model KIMI_K2_INSTRUCT_0905 = new Model
    {
        Id = "moonshotai/kimi-k2-instruct-0905",
        Object = "model",
        Created = 1757046093,
        OwnedBy = "Moonshot AI",
        Active = true,
        ContextWindow = 262144,
        PublicApps = null,
        MaxCompletionTokens = 16384
    };

    public static readonly Model LLAMA_3_3_70B_VERSATILE = new Model
    {
        Id = "llama-3.3-70b-versatile",
        Object = "model",
        Created = 1733447754,
        OwnedBy = "Meta",
        Active = true,
        ContextWindow = 131072,
        PublicApps = null,
        MaxCompletionTokens = 32768
    };
}
