// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Net.Http.Headers;
using Groq.Core.Configurations;
using Groq.Core.Interfaces;
using Groq.Core.Providers;

namespace Groq.Core.Clients;

/// <summary>
/// Main client for interacting with the Groq API, providing access to chat completions,
/// audio synthesis, vision analysis, function calling, and text generation services.
/// </summary>
/// <remarks>
/// This class serves as a facade that provides unified access to all Groq API functionality
/// through specialized client instances. It supports multiple initialization patterns including
/// direct configuration, HttpClient injection, and full dependency injection.
/// </remarks>
public sealed class GroqClient
{
    /// <summary>
    /// Gets the client for chat completion operations.
    /// </summary>
    /// <value>
    /// A <see cref="ChatCompletionClient"/> instance for performing chat-based LLM interactions.
    /// </value>
    // ReSharper disable once MemberCanBePrivate.Global
    public ChatCompletionClient Chat { get; }

    /// <summary>
    /// Gets the client for audio synthesis operations.
    /// </summary>
    /// <value>
    /// An <see cref="AudioClient"/> instance for text-to-speech and audio generation.
    /// </value>
    public AudioClient Audio { get; }

    /// <summary>
    /// Gets the client for vision and image analysis operations.
    /// </summary>
    /// <value>
    /// A <see cref="VisionClient"/> instance for analyzing images with vision-capable models.
    /// </value>
    public VisionClient Vision { get; }

    /// <summary>
    /// Gets the client for function calling and tool use operations.
    /// </summary>
    /// <value>
    /// A <see cref="ToolClient"/> instance for executing functions and using tools with LLMs.
    /// </value>
    public ToolClient Tools { get; }

    /// <summary>
    /// Gets the provider for abstracted text generation operations.
    /// </summary>
    /// <value>
    /// An <see cref="ILlmTextProvider"/> instance providing a simplified interface for text generation.
    /// </value>
    public ILlmTextProvider LlmTextProvider { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GroqClient"/> class with the specified settings.
    /// </summary>
    /// <param name="options">The configuration options for the Groq API client.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="options"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the API key in <paramref name="options"/> is null or empty.</exception>
    /// <remarks>
    /// This constructor creates a new <see cref="HttpClient"/> instance configured with the base URL,
    /// timeout, and authorization header from the provided options. This constructor should only be used if you are
    /// using an application that doesn't support DI like Console Applications.
    /// For any Hosted Application that supports DI, we recommend using the <c>Groq.Extensions.DependencyInjection</c>
    /// package to be able to register the GroqClient in the service collection and take advantage of HttpClientFactory
    /// for proper HttpClient lifecycle management and connection pooling.
    /// </remarks>
    public GroqClient(GroqOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrEmpty(options.ApiKey);

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.BaseUrl),
            Timeout = options.Timeout,
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", options.ApiKey)
            }
        };

        Chat = new ChatCompletionClient(httpClient);
        Audio = new AudioClient(httpClient);
        Vision = new VisionClient(Chat);
        Tools = new ToolClient(Chat);
        LlmTextProvider = new LlmTextProvider(Chat, options.Model);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GroqClient"/> class with fully injected dependencies.
    /// </summary>
    /// <param name="chatCompletionClient">The client for chat completion operations.</param>
    /// <param name="audioClient">The client for audio synthesis operations.</param>
    /// <param name="visionClient">The client for vision and image analysis operations.</param>
    /// <param name="toolClient">The client for function calling and tool use operations.</param>
    /// <param name="llmTextProvider">The provider for abstracted text generation operations.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the parameters is null.
    /// </exception>
    /// <remarks>
    /// This constructor enables full dependency injection for testing scenarios and advanced
    /// configuration patterns. All specialized clients and providers must be pre-configured
    /// and injected, providing maximum flexibility and testability.
    /// </remarks>
    public GroqClient(
        ChatCompletionClient chatCompletionClient,
        AudioClient audioClient,
        VisionClient visionClient,
        ToolClient toolClient,
        ILlmTextProvider llmTextProvider)
    {
        ArgumentNullException.ThrowIfNull(chatCompletionClient);
        ArgumentNullException.ThrowIfNull(audioClient);
        ArgumentNullException.ThrowIfNull(visionClient);
        ArgumentNullException.ThrowIfNull(toolClient);
        ArgumentNullException.ThrowIfNull(llmTextProvider);

        Chat = chatCompletionClient;
        Audio = audioClient;
        Vision = visionClient;
        Tools = toolClient;
        LlmTextProvider = llmTextProvider;
    }
}

