// Licensed to the .Net Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Groq.Core.Models;

/// <summary>
///     Contains text generation and chat completion models including general-purpose LLMs,
///     specialized content moderation models, and advanced reasoning models.
/// </summary>
public static class ChatModels
{
    /// <summary>
    ///     OpenAI's flagship open-weight Mixture-of-Experts (MoE) model with 120B total parameters.
    ///     Designed for high-capability agentic use, matching or surpassing proprietary models on many benchmarks.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> MoE with 120B total parameters (5.1B active per forward pass), 36 layers, 128
    ///         experts, Top-4 routing.
    ///     </para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 65,536 tokens</para>
    ///     <para><b>Token Speed:</b> ~500 tokens per second</para>
    ///     <para><b>Pricing:</b> Input $0.15/1M tokens, Output $0.60/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, Browser Search, Code Execution, JSON Object Mode, JSON Schema Mode, Reasoning</para>
    ///     <para><b>Performance:</b> MMLU 90.0%, SWE-Bench Verified 62.4%, HealthBench 57.6%, MMMLU 81.3%</para>
    ///     <para>
    ///         <b>Use Cases:</b> Frontier-grade agentic applications, advanced research & scientific computing,
    ///         high-accuracy math/coding, multilingual AI assistants
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Use variable reasoning modes (low/medium/high); leverage Harmony chat format; utilize
    ///         full 131K context window
    ///     </para>
    /// </remarks>
    public static readonly Model OPENAI_GPT_OSS_120B = new()
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

    /// <summary>
    ///     OpenAI's compact open-weight Mixture-of-Experts (MoE) model with 20B total parameters.
    ///     Optimized for cost-efficient deployment and agentic workflows with small memory footprint.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> MoE with 20B total parameters (3.6B active per forward pass), 24 layers, 32
    ///         experts, Top-4 routing.
    ///     </para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 65,536 tokens</para>
    ///     <para><b>Token Speed:</b> ~1000 tokens per second</para>
    ///     <para><b>Pricing:</b> Input $0.075/1M tokens (Cached $0.037), Output $0.30/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, Browser Search, Code Execution, JSON Object Mode, JSON Schema Mode, Reasoning</para>
    ///     <para><b>Performance:</b> MMLU 85.3%, SWE-Bench Verified 60.7%, AIME 2025 98.7%, MMMLU 75.7%</para>
    ///     <para>
    ///         <b>Use Cases:</b> Low-latency agentic applications, affordable reasoning & coding, tool-augmented
    ///         applications, long-context processing
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Use variable reasoning modes; provide clear tool definitions; structure complex tasks
    ///         into steps; leverage multilingual capabilities
    ///     </para>
    /// </remarks>
    public static readonly Model OPENAI_GPT_OSS_20B = new()
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

    /// <summary>
    ///     Meta's specialized 86M parameter classifier for detecting prompt attacks in LLM applications.
    ///     Provides multilingual support across 8 languages with superior accuracy.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> Based on Microsoft's mDeBERTa-base (86M parameters) with adversarial-attack
    ///         resistant tokenization.
    ///     </para>
    ///     <para><b>Context Window:</b> 512 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 512 tokens</para>
    ///     <para><b>Pricing:</b> Input/Output $0.04/1M tokens each</para>
    ///     <para><b>Capabilities:</b> JSON Object Mode, Content Moderation</para>
    ///     <para>
    ///         <b>Performance:</b> 99.8% AUC English jailbreak detection, 97.5% recall at 1% FPR, 81.2% attack prevention
    ///         rate
    ///     </para>
    ///     <para>
    ///         <b>Use Cases:</b> Prompt attack detection (injections, jailbreaks), LLM pipeline security, multilingual
    ///         content filtering
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Split long inputs into segments; use 86M version for multilingual support; implement
    ///         as part of multi-layered security
    ///     </para>
    ///     <para><b>Languages:</b> 8 languages supported</para>
    /// </remarks>
    public static readonly Model LLAMA_PROMPT_GUARD_2_86M = new()
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

    /// <summary>
    ///     Alibaba Cloud's Qwen 3 32B with dual-mode system supporting both thinking mode for complex reasoning
    ///     and non-thinking mode for efficient dialogue within a single model.
    /// </summary>
    /// <remarks>
    ///     <para><b>Model Architecture:</b> 32 billion parameters with unique dual-mode system (thinking/non-thinking modes).</para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 40,960 tokens</para>
    ///     <para><b>Token Speed:</b> ~400 tokens per second</para>
    ///     <para><b>Pricing:</b> Input $0.29/1M tokens, Output $0.59/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, JSON Object Mode, Reasoning</para>
    ///     <para><b>Performance:</b> ArenaHard 93.8%, AIME 2024 81.4%, LiveCodeBench 65.7%, AIME 2025 72.9%</para>
    ///     <para>
    ///         <b>Use Cases:</b> Complex problem solving (multi-step reasoning, math, coding), natural dialogue & content
    ///         creation, multilingual content
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Use thinking mode with reasoning_effort="default" for complex tasks; non-thinking mode
    ///         for general dialogue; supports 100+ languages
    ///     </para>
    ///     <para><b>Reasoning Modes:</b> Supports seamless switching between thinking and non-thinking modes</para>
    /// </remarks>
    public static readonly Model QWEN3_32B = new()
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

    /// <summary>
    ///     Meta's Llama 3.1 8B optimized for low-latency, high-quality responses.
    ///     Perfect for real-time conversational interfaces with significant cost savings.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> 8 billion parameters with Grouped-Query Attention (GQA) for improved inference
    ///         scalability.
    ///     </para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 131,072 tokens</para>
    ///     <para><b>Token Speed:</b> ~560 tokens per second</para>
    ///     <para><b>Pricing:</b> Input $0.05/1M tokens, Output $0.08/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, JSON Object Mode</para>
    ///     <para><b>Performance:</b> MMLU 69.4%, HumanEval 72.6%, MATH 51.9%, TriviaQA-Wiki 77.6%</para>
    ///     <para>
    ///         <b>Use Cases:</b> Real-time applications (content moderation, interactive education, dynamic content
    ///         generation), high-volume processing
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Leverage large context window; simplify complex queries; enable JSON mode for
    ///         structured data; include examples
    ///     </para>
    ///     <para><b>Training:</b> Fine-tuned using SFT and RLHF for enhanced response accuracy</para>
    /// </remarks>
    public static readonly Model LLAMA_3_1_8B_INSTANT = new()
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

    /// <summary>
    ///     Meta's specialized 12B parameter natively multimodal content moderation model.
    ///     Identifies and classifies harmful content using MLCommons Taxonomy framework.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> Based on Llama 4 Scout architecture with 12 billion parameters, fine-tuned for
    ///         content safety.
    ///     </para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 1,024 tokens</para>
    ///     <para><b>Token Speed:</b> ~1200 tokens per second</para>
    ///     <para><b>Input Types:</b> Text and images (max 5 images, max 20 MB file size)</para>
    ///     <para><b>Pricing:</b> Input/Output $0.20/1M tokens each</para>
    ///     <para><b>Capabilities:</b> JSON Object Mode, Content Moderation (multimodal)</para>
    ///     <para><b>Performance:</b> High accuracy in harmful content identification with low false positive rate</para>
    ///     <para>
    ///         <b>Use Cases:</b> Content moderation (chatbots, forums, platforms), AI safety (prompt screening, output
    ///         filtering), automated content screening
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Configure safety thresholds appropriately; provide sufficient context; test with up to
    ///         5 input images
    ///     </para>
    ///     <para><b>Framework:</b> Uses MLCommons Taxonomy for content classification</para>
    /// </remarks>
    public static readonly Model LLAMA_GUARD_4_12B = new()
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

    /// <summary>
    ///     Meta's specialized 22M parameter classifier for detecting prompt attacks.
    ///     Optimized for latency with 75% reduction in compute costs compared to larger models.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> Based on Microsoft's DeBERTa-xsmall (22M parameters) with adversarial-attack
    ///         resistant tokenization.
    ///     </para>
    ///     <para><b>Context Window:</b> 512 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 512 tokens</para>
    ///     <para><b>Pricing:</b> Input/Output $0.03/1M tokens each</para>
    ///     <para><b>Capabilities:</b> Content Moderation</para>
    ///     <para>
    ///         <b>Performance:</b> 99.5% AUC English jailbreak detection, 88.7% recall at 1% FPR, 78.4% attack prevention
    ///         rate, 75% latency reduction
    ///     </para>
    ///     <para>
    ///         <b>Use Cases:</b> Prompt attack detection (injections, jailbreaks), LLM pipeline security, real-time input
    ///         analysis
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Split long inputs into segments; use 22M version for better latency/efficiency;
    ///         implement multi-layered security
    ///     </para>
    ///     <para><b>Languages:</b> Optimized for English language attack detection</para>
    /// </remarks>
    public static readonly Model LLAMA_PROMPT_GUARD_2_22M = new()
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

    /// <summary>
    ///     Moonshot AI's Kimi K2 0905 - Enhanced MoE model with 1T total parameters featuring superior
    ///     frontend development and tool calling performance for building sophisticated AI agents.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> MoE with 1 trillion total parameters (32B activated), 384 experts with 8 experts
    ///         selected per token.
    ///     </para>
    ///     <para><b>Context Window:</b> 262,144 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 16,384 tokens</para>
    ///     <para><b>Token Speed:</b> ~200 tokens per second</para>
    ///     <para><b>Pricing:</b> Input $1.00/1M tokens (Cached $0.50), Output $3.00/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, JSON Object Mode, JSON Schema Mode</para>
    ///     <para><b>Performance:</b> LiveCodeBench 53.7%, SWE-bench Verified 65.8%, MMLU 89.5%, Tau2 retail 70.6%</para>
    ///     <para>
    ///         <b>Use Cases:</b> Enhanced frontend development (React, Vue, Angular), advanced agent scaffolds, tool calling
    ///         excellence, full-stack development
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Specify frameworks clearly; leverage improved scaffold integration; provide
    ///         comprehensive tool schemas; use full 256K context
    ///     </para>
    ///     <para><b>Training:</b> Trained with innovative Muon optimizer for zero training instability</para>
    /// </remarks>
    public static readonly Model KIMI_K2_INSTRUCT_0905 = new()
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

    /// <summary>
    ///     Meta's Llama 3.3 70B advanced multilingual model optimized for diverse NLP tasks.
    ///     Delivers exceptional performance with high efficiency across various benchmarks.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> 70 billion parameters with Grouped-Query Attention (GQA) for enhanced inference
    ///         scalability.
    ///     </para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 32,768 tokens</para>
    ///     <para><b>Token Speed:</b> ~280 tokens per second</para>
    ///     <para><b>Pricing:</b> Input $0.59/1M tokens, Output $0.79/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, JSON Object Mode</para>
    ///     <para><b>Performance:</b> MMLU 86.0%, HumanEval 88.4%, MATH 77.0%, MGSM 91.1%</para>
    ///     <para>
    ///         <b>Use Cases:</b> Advanced language understanding, code generation & problem solving, multilingual
    ///         applications
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Clearly specify task instructions; provide sufficient context; define tool/function
    ///         definitions clearly
    ///     </para>
    ///     <para><b>Training:</b> Fine-tuned using SFT and RLHF for alignment with human preferences</para>
    /// </remarks>
    public static readonly Model LLAMA_3_3_70B_VERSATILE = new()
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
