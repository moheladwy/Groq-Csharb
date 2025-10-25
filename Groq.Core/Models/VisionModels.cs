// Licensed to the .Net Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Groq.Core.Models;

/// <summary>
///     Contains Meta's Llama 4 natively multimodal vision models that enable text and image understanding.
///     These models combine visual recognition with natural language processing for advanced multimodal AI applications.
/// </summary>
public static class VisionModels
{
    /// <summary>
    ///     Meta's Llama 4 Scout - A natively multimodal model with 17B activated parameters (109B total).
    ///     Optimized for fast inference with 16 experts for efficient text and image understanding.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> Auto-regressive MoE with 17B activated parameters (109B total), 16 experts with
    ///         early fusion for native multimodality.
    ///     </para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 8,192 tokens</para>
    ///     <para><b>Token Speed:</b> ~750 tokens per second</para>
    ///     <para><b>Input Types:</b> Text and images (max 5 images, max 20 MB file size)</para>
    ///     <para><b>Pricing:</b> Input $0.11/1M tokens, Output $0.34/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, JSON Object Mode, JSON Schema Mode</para>
    ///     <para><b>Performance:</b> MMLU Pro 52.2, ChartQA 88.8, DocVQA 94.4 anls</para>
    ///     <para>
    ///         <b>Languages:</b> 12 languages (Arabic, English, French, German, Hindi, Indonesian, Italian, Portuguese,
    ///         Spanish, Tagalog, Thai, Vietnamese)
    ///     </para>
    ///     <para>
    ///         <b>Use Cases:</b> Multimodal assistant applications (visual recognition, image reasoning, captioning), code
    ///         generation & technical tasks, long-context applications
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Use system prompts for improved steerability; implement Llama Guard for safety;
    ///         supports up to 5 image inputs; deploy with appropriate safeguards
    ///     </para>
    ///     <para><b>Knowledge Cutoff:</b> August 2024</para>
    ///     <para><b>License Note:</b> EU restrictions apply - see Llama 4 Community License Agreement</para>
    /// </remarks>
    public static readonly Model LLAMA_4_SCOUT_17B_16E_INSTRUCT = new()
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

    /// <summary>
    ///     Meta's Llama 4 Maverick - A natively multimodal model with 17B activated parameters (400B total).
    ///     Features 128 experts for industry-leading multimodal performance with text and image understanding.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Model Architecture:</b> Auto-regressive MoE with 17B activated parameters (400B total), 128 experts with
    ///         early fusion for native multimodality.
    ///     </para>
    ///     <para><b>Context Window:</b> 131,072 tokens</para>
    ///     <para><b>Max Output Tokens:</b> 8,192 tokens</para>
    ///     <para><b>Token Speed:</b> ~600 tokens per second</para>
    ///     <para><b>Input Types:</b> Text and images (max 5 images, max 20 MB file size)</para>
    ///     <para><b>Pricing:</b> Input $0.20/1M tokens, Output $0.60/1M tokens</para>
    ///     <para><b>Capabilities:</b> Tool Use, JSON Object Mode, JSON Schema Mode</para>
    ///     <para><b>Performance:</b> MMLU Pro 59.6, ChartQA 90.0, DocVQA 94.4 anls</para>
    ///     <para>
    ///         <b>Languages:</b> 12 languages (Arabic, English, French, German, Hindi, Indonesian, Italian, Portuguese,
    ///         Spanish, Tagalog, Thai, Vietnamese)
    ///     </para>
    ///     <para>
    ///         <b>Use Cases:</b> Multimodal assistant applications (visual recognition, image reasoning, captioning), code
    ///         generation & technical tasks, long-context applications
    ///     </para>
    ///     <para>
    ///         <b>Best Practices:</b> Use system prompts for improved steerability; implement Llama Guard for safety;
    ///         supports up to 5 image inputs; deploy with appropriate safeguards
    ///     </para>
    ///     <para><b>Knowledge Cutoff:</b> August 2024</para>
    ///     <para><b>License Note:</b> EU restrictions apply - see Llama 4 Community License Agreement</para>
    /// </remarks>
    public static readonly Model LLAMA_4_MAVERICK_17B_128E_INSTRUCT = new()
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
