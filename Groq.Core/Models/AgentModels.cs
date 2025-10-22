// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Groq.Core.Models;

/// <summary>
///     Contains Groq Compound agent models that integrate language models with external tools
///     for enhanced capabilities including web search, code execution, and browser automation.
/// </summary>
public static class AgentModels
{
    /// <summary>
    ///     Groq's Compound Mini system integrating GPT-OSS 120B and Llama 3.3 70B models with external tools.
    ///     This model provides real-time data access and environment interaction with 3x lower latency compared to Compound.
    /// </summary>
    /// <remarks>
    ///     <para><b>Model Architecture:</b> Powered by Llama 3.3 70B and GPT-OSS 120B for intelligent reasoning and tool use.</para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 8,192 tokens</para>
    ///     <para><b>Token Speed:</b> ~450 tokens per second</para>
    ///     <para><b>Key Limitation:</b> Can only use one tool per request, but offers 3x lower latency than groq/compound.</para>
    ///     <para>
    ///         <b>Capabilities:</b> Web Search, Code Execution (E2B), Visit Website, Browser Automation, Wolfram Alpha, JSON
    ///         Object Mode
    ///     </para>
    ///     <para>
    ///         <b>Use Cases:</b> Realtime web search, code execution and generation, technical tasks with high-quality
    ///         multilingual support
    ///     </para>
    ///     <para>
    ///         <b>Pricing:</b> Variable based on underlying models (GPT-OSS-120B: $0.15/$0.60 per 1M tokens, Llama 3.3 70B:
    ///         $0.59/$0.79 per 1M tokens) and tool usage
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Use system prompts for improved steerability; implement Llama Guard for content
    ///         filtering; not for HIPAA protected health information
    ///     </para>
    /// </remarks>
    public static readonly Model GROQ_COMPOUND_MINI = new()
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

    /// <summary>
    ///     Groq's Compound system integrating GPT-OSS 120B and Llama 4 Scout models with external tools.
    ///     This model provides unified tool integration and orchestration for real-time data access and environment
    ///     interaction.
    /// </summary>
    /// <remarks>
    ///     <para><b>Model Architecture:</b> Powered by Llama 4 Scout and GPT-OSS 120B for intelligent reasoning and tool use.</para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 8,192 tokens</para>
    ///     <para><b>Token Speed:</b> ~450 tokens per second</para>
    ///     <para><b>Key Advantage:</b> Supports multiple tools per request with comprehensive tool orchestration capabilities.</para>
    ///     <para>
    ///         <b>Capabilities:</b> Web Search, Code Execution (E2B), Visit Website, Browser Automation, Wolfram Alpha, JSON
    ///         Object Mode
    ///     </para>
    ///     <para>
    ///         <b>Use Cases:</b> Realtime web search, code execution and generation, technical tasks with high-quality
    ///         multilingual support
    ///     </para>
    ///     <para>
    ///         <b>Pricing:</b> Variable based on underlying models (GPT-OSS-120B: $0.15/$0.60 per 1M tokens, Llama 4 Scout:
    ///         $0.11/$0.34 per 1M tokens) and tool usage
    ///     </para>
    ///     <para>
    ///         <b>Performance:</b> Outperforms GPT-4o-search-preview and GPT-4o-mini-search-preview on RealtimeEval
    ///         benchmark
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Use system prompts for improved steerability; implement Llama Guard for content
    ///         filtering; not for HIPAA protected health information
    ///     </para>
    /// </remarks>
    public static readonly Model GROQ_COMPOUND = new()
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
