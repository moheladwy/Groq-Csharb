// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Net.Http.Headers;
using Groq.Core.Interfaces;
using Groq.Core.Providers;
using Groq.Core.Settings;

namespace Groq.Core.Clients;

public sealed class GroqClient
{
    // ReSharper disable once MemberCanBePrivate.Global
    public ChatCompletionClient Chat { get; }
    public AudioClient Audio { get; }
    public VisionClient Vision { get; }
    public ToolClient Tools { get; }
    public ILlmTextProvider LlmTextProvider { get; }

    public GroqClient(GroqSettings options)
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

    public GroqClient(HttpClient httpClient, string? model = null)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        Chat = new ChatCompletionClient(httpClient);
        Audio = new AudioClient(httpClient);
        Vision = new VisionClient(Chat);
        Tools = new ToolClient(Chat);
        LlmTextProvider = new LlmTextProvider(Chat, model);
    }

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

    public static GroqClient CreateGroqClientInstance(GroqSettings options) => new GroqClient(options);
}

