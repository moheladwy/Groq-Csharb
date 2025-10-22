// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Groq.Core.Models;

/// <summary>
/// Contains audio processing models for speech-to-text (STT) and text-to-speech (TTS) capabilities.
/// Includes OpenAI's Whisper models for transcription and PlayAI's models for voice synthesis.
/// </summary>
public static class AudioModels
{
    /// <summary>
    /// OpenAI's fastest speech recognition model optimized for speed while maintaining high accuracy.
    /// Perfect for time-sensitive applications requiring immediate transcription.
    /// </summary>
    /// <remarks>
    /// <para><b>Model Architecture:</b> Optimized transformer architecture with streamlined processing for enhanced speed.</para>
    /// <para><b>Speed Factor:</b> 216x speed - fastest in the Whisper family</para>
    /// <para><b>Context Window:</b> 448 tokens (optimized for 30-second audio segments, minimum 10 seconds)</para>
    /// <para><b>Supported Formats:</b> FLAC, MP3, M4A, MPEG, MPGA, OGG, WAV, WEBM</para>
    /// <para><b>Languages:</b> 99+ languages with multilingual support</para>
    /// <para><b>Max File Size:</b> 100 MB</para>
    /// <para><b>Pricing:</b> $0.04 per hour of audio</para>
    /// <para><b>Performance:</b> High accuracy across diverse audio conditions with reduced latency</para>
    /// <para><b>Use Cases:</b> Real-time applications (live streaming, broadcast captioning), high-volume batch processing, cost-effective solutions</para>
    /// <para><b>Best Practices:</b> Ideal for fast transcription requirements, real-time processing, and budget-conscious applications</para>
    /// </remarks>
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

    /// <summary>
    /// OpenAI's most advanced speech recognition model delivering state-of-the-art accuracy.
    /// The gold standard for automatic speech recognition requiring the highest possible accuracy.
    /// </summary>
    /// <remarks>
    /// <para><b>Model Architecture:</b> Transformer-based encoder-decoder with 1550M parameters and sophisticated attention mechanism.</para>
    /// <para><b>Model Size:</b> 1550M parameters</para>
    /// <para><b>Speed Factor:</b> 189x speed</para>
    /// <para><b>Context Window:</b> 448 tokens (optimized for 30-second audio segments, minimum 10 seconds)</para>
    /// <para><b>Supported Formats:</b> FLAC, MP3, M4A, MPEG, MPGA, OGG, WAV, WEBM</para>
    /// <para><b>Languages:</b> 99+ languages with multilingual support</para>
    /// <para><b>Max File Size:</b> 100 MB</para>
    /// <para><b>Pricing:</b> $0.111 per hour of audio</para>
    /// <para><b>Performance Metrics:</b> Short-form: 8.4% WER, Sequential long-form: 10.0% WER, Chunked long-form: 11.0% WER</para>
    /// <para><b>Use Cases:</b> High-accuracy transcription (legal, medical), multilingual applications, challenging audio conditions (noise, overlapping speech)</para>
    /// <para><b>Best Practices:</b> Prioritize accuracy over speed; leverage extensive language support; excellent for difficult audio scenarios</para>
    /// </remarks>
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

    /// <summary>
    /// PlayAI Dialog v1.0 - A generative AI model for text-to-speech with human-like audio quality.
    /// Supports creative content generation, voice cloning, and customizable voice parameters.
    /// </summary>
    /// <remarks>
    /// <para><b>Model Architecture:</b> Transformer architecture optimized for high-quality speech output with voice cloning capabilities.</para>
    /// <para><b>Context Window:</b> 8192 characters</para>
    /// <para><b>Input:</b> Text (recommended under 10K characters for best results)</para>
    /// <para><b>Output:</b> Human-like audio with customizable tone, style, and narrative focus</para>
    /// <para><b>Languages:</b> English (primary support)</para>
    /// <para><b>Pricing:</b> $50.00 per million characters (20,000 characters per $1)</para>
    /// <para><b>Training Data:</b> Millions of audio samples from diverse sources including video, audio works, and interactive dialogue datasets</para>
    /// <para><b>Use Cases:</b> Creative content generation, voice agentic experiences, conversational AI, customer support, accessibility tools</para>
    /// <para><b>Best Practices:</b> Use voice cloning and parameter customization; consider cultural sensitivity; keep input under 10K characters; comply with Play.ht Terms of Service</para>
    /// <para><b>Limitations:</b> May reflect cultural biases in pronunciations and accents; outputs can be unpredictable requiring human curation</para>
    /// <para><b>License:</b> PlayAI-Groq Commercial License</para>
    /// </remarks>
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

    /// <summary>
    /// PlayAI Dialog v1.0 Arabic variant - Specialized text-to-speech model with native Arabic language support.
    /// Provides human-like audio quality with customizable voice parameters for Arabic content.
    /// </summary>
    /// <remarks>
    /// <para><b>Model Architecture:</b> Transformer architecture optimized for high-quality speech output with voice cloning capabilities.</para>
    /// <para><b>Context Window:</b> 8192 characters</para>
    /// <para><b>Input:</b> Text (recommended under 10K characters for best results)</para>
    /// <para><b>Output:</b> Human-like audio with customizable tone, style, and narrative focus</para>
    /// <para><b>Languages:</b> Arabic (specialized) and English</para>
    /// <para><b>Pricing:</b> $50.00 per million characters (20,000 characters per $1)</para>
    /// <para><b>Training Data:</b> Millions of audio samples from diverse sources including video, audio works, and interactive dialogue datasets</para>
    /// <para><b>Use Cases:</b> Arabic creative content generation, voice agentic experiences, conversational AI, customer support for Arabic speakers, accessibility tools</para>
    /// <para><b>Best Practices:</b> Use voice cloning and parameter customization; consider cultural sensitivity for Arabic contexts; keep input under 10K characters; comply with Play.ht Terms of Service</para>
    /// <para><b>Limitations:</b> May reflect cultural biases in pronunciations and accents; outputs can be unpredictable requiring human curation</para>
    /// <para><b>License:</b> PlayAI-Groq Commercial License</para>
    /// </remarks>
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
