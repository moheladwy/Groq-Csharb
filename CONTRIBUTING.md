# Contributing to GroqApiLibrary

Thank you for your interest in contributing to GroqApiLibrary! This document provides guidelines and instructions for contributing to this project.

## 🌟 Ways to Contribute

There are many ways you can contribute to this project:

-   🐛 **Report bugs** - Help us identify issues
-   ✨ **Suggest features** - Share your ideas for improvements
-   📝 **Improve documentation** - Help others understand the library
-   💻 **Submit code** - Fix bugs or implement new features
-   🧪 **Write tests** - Improve code coverage and reliability
-   📖 **Create examples** - Show others how to use the library

## 📋 Table of Contents

-   [Code of Conduct](#code-of-conduct)
-   [Getting Started](#getting-started)
-   [Development Setup](#development-setup)
-   [Project Structure](#project-structure)
-   [Coding Standards](#coding-standards)
-   [Testing Guidelines](#testing-guidelines)
-   [Pull Request Process](#pull-request-process)
-   [Priority Areas](#priority-areas)
-   [Documentation Guidelines](#documentation-guidelines)
-   [Community](#community)

## 📜 Code of Conduct

This project adheres to the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code. Please report unacceptable behavior to [j@gravelle.us](mailto:j@gravelle.us).

Key principles:

-   **Be respectful** - Treat everyone with respect and kindness
-   **Be collaborative** - Work together and help each other
-   **Be inclusive** - Welcome diverse perspectives and backgrounds
-   **Be professional** - Keep discussions focused and constructive

Please read the full [Code of Conduct](CODE_OF_CONDUCT.md) for detailed guidelines and enforcement policies.

## 🚀 Getting Started

### Before You Start

1. **Check existing issues** - Look for existing issues or discussions about your idea
2. **Create an issue first** - For significant changes, create an issue to discuss your approach
3. **Fork the repository** - Create your own fork to work on
4. **Follow the guidelines** - Read this document carefully

### First-Time Contributors

If this is your first contribution, look for issues labeled with:

-   `good first issue` - Good for newcomers
-   `help wanted` - Extra attention needed
-   `documentation` - Documentation improvements

## 💻 Development Setup

### Prerequisites

-   **.NET 9.0 SDK** or later
-   **Git** for version control
-   **IDE** - Visual Studio 2022, VS Code, or Rider recommended
-   **Groq API Key** for testing (get one at [console.groq.com](https://console.groq.com))

### Setup Steps

1. **Fork and Clone**

    ```bash
    git clone https://github.com/YOUR_USERNAME/GroqApiLibrary.git
    cd GroqApiLibrary
    ```

2. **Restore Dependencies**

    ```bash
    dotnet restore
    ```

3. **Build the Project**

    ```bash
    dotnet build
    ```

4. **Set Up API Key** (for testing)

    ```bash
    # Linux/macOS
    export GROQ_API_KEY="your-api-key-here"

    # Windows PowerShell
    $env:GROQ_API_KEY="your-api-key-here"
    ```

5. **Run Tests**
    ```bash
    dotnet test
    ```

## 📁 Project Structure

```
GroqApiLibrary/
├── Clients/              # API client implementations
│   ├── AudioClient.cs
│   ├── ChatCompletionClient.cs
│   ├── ToolClient.cs
│   └── VisionClient.cs
├── Extensions/           # Dependency injection extensions
│   └── RegisterGroq.cs
├── Interfaces/           # Interface definitions
│   └── ILlmTextProvider.cs
├── Models/              # Model definitions and DTOs
│   ├── AgentModels.cs
│   ├── AudioModels.cs
│   ├── ChatModels.cs
│   ├── Function.cs
│   ├── Model.cs
│   ├── ModelListResponse.cs
│   ├── Tool.cs
│   └── VisionModels.cs
├── Providers/           # Service providers
│   └── LlmTextProvider.cs
├── Settings/            # Configuration and constants
│   ├── Endpoints.cs
│   ├── LlmRoles.cs
│   ├── VisionSettings.cs
│   └── Voice/
│       ├── ArabicVoices.cs
│       └── EnglishVoices.cs
└── GroqApiLibrary.csproj
```

## 📝 Coding Standards

### General Guidelines

1. **Follow C# Conventions**

    - Use PascalCase for classes, methods, and properties
    - Use camelCase for local variables and parameters
    - Use UPPER_CASE for constants
    - Prefix interface names with `I`

2. **Code Style**

    - Use 4 spaces for indentation (no tabs)
    - Place opening braces on new lines
    - Keep lines under 120 characters when possible
    - Use meaningful variable and method names

3. **XML Documentation**
    - All public APIs must have XML documentation comments
    - Include `<summary>`, `<param>`, `<returns>`, and `<exception>` tags
    - Add `<remarks>` for additional context and examples
    - Document edge cases and important behaviors

### Example Code Style

```csharp
/// <summary>
/// Creates a chat completion using the Groq API.
/// </summary>
/// <param name="request">The request object containing chat completion parameters.</param>
/// <returns>The API response as a JsonObject.</returns>
/// <exception cref="HttpRequestException">Thrown when the API request fails.</exception>
/// <remarks>
/// This method supports streaming responses by setting the stream parameter in the request.
/// For best results, use appropriate temperature and max_tokens settings.
/// </remarks>
public async Task<JsonObject?> CreateChatCompletionAsync(JsonObject request)
{
    ArgumentNullException.ThrowIfNull(request, nameof(request));

    var response = await _httpClient.PostAsJsonAsync(Endpoints.ChatCompletionsEndpoint, request);
    if (!response.IsSuccessStatusCode)
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException(
            $"API request failed with status code {response.StatusCode}. Response content: {errorContent}");
    }

    return await response.Content.ReadFromJsonAsync<JsonObject>();
}
```

### Async/Await Best Practices

-   Use `async`/`await` for all I/O operations
-   Avoid `Task.Wait()` or `Task.Result`
-   Use `ConfigureAwait(false)` in library code when appropriate
-   Return `Task` or `Task<T>` from async methods
-   Name async methods with `Async` suffix

### Error Handling

-   Validate input parameters using `ArgumentNullException.ThrowIfNull()` or `ArgumentException.ThrowIfNullOrEmpty()`
-   Provide meaningful error messages
-   Let framework exceptions bubble up (don't catch and rethrow unnecessarily)
-   Document all exceptions in XML comments

## 🧪 Testing Guidelines

### Test Structure

-   Follow the **Arrange-Act-Assert** pattern
-   Use descriptive test names: `MethodName_Scenario_ExpectedBehavior`
-   Group related tests in test classes
-   Use `[Fact]` for simple tests, `[Theory]` for parameterized tests

### Example Test

```csharp
[Fact]
public async Task CreateChatCompletionAsync_WithValidRequest_ReturnsResponse()
{
    // Arrange
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://api.groq.com/openai/v1/")
    };
    httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("GROQ_API_KEY"));

    var client = new ChatCompletionClient(httpClient);
    var request = new JsonObject
    {
        ["model"] = "llama-3.1-8b-instant",
        ["messages"] = new JsonArray
        {
            new JsonObject { ["role"] = "user", ["content"] = "Hello!" }
        }
    };

    // Act
    var response = await client.CreateChatCompletionAsync(request);

    // Assert
    Assert.NotNull(response);
    Assert.Contains("choices", response.AsObject().Select(kvp => kvp.Key));
}
```

### Test Coverage

-   Aim for high test coverage (>80%)
-   Test happy paths and error cases
-   Test edge cases and boundary conditions
-   Mock external dependencies when appropriate
-   Use integration tests sparingly (they require API keys)

## 🔄 Pull Request Process

### Before Submitting

1. **Create a feature branch**

    ```bash
    git checkout -b feature/your-feature-name
    ```

2. **Make your changes**

    - Write clean, documented code
    - Follow coding standards
    - Add or update tests
    - Update documentation

3. **Commit your changes**

    ```bash
    git add .
    git commit -m "feat: add new feature description"
    ```

    Use conventional commit messages:

    - `feat:` - New feature
    - `fix:` - Bug fix
    - `docs:` - Documentation changes
    - `test:` - Test additions or changes
    - `refactor:` - Code refactoring
    - `style:` - Code style changes (formatting)
    - `chore:` - Maintenance tasks

4. **Run all tests**

    ```bash
    dotnet test
    ```

5. **Build the project**
    ```bash
    dotnet build --configuration Release
    ```

### Submitting Your PR

1. **Push to your fork**

    ```bash
    git push origin feature/your-feature-name
    ```

2. **Create a Pull Request**

    - Go to the original repository on GitHub
    - Click "New Pull Request"
    - Select your fork and branch
    - Fill out the PR template

3. **PR Description Should Include**
    - Clear description of changes
    - Link to related issue (if applicable)
    - Screenshots (for UI changes)
    - Breaking changes (if any)
    - Testing performed

### PR Review Process

-   Maintainers will review your PR
-   Address any feedback or requested changes
-   Keep your PR up to date with the main branch
-   Once approved, a maintainer will merge your PR

### PR Checklist

-   [ ] Code follows the project's coding standards
-   [ ] All tests pass
-   [ ] New tests added for new functionality
-   [ ] XML documentation added/updated
-   [ ] README updated (if needed)
-   [ ] No breaking changes (or clearly documented)
-   [ ] Commit messages follow conventional format

## 🎯 Priority Areas

We're especially looking for help with these features:

### 🔴 High Priority

1. **Responses API Implementation**

    - Most requested feature
    - Complex but high value
    - Skills needed: C#, async programming, API design

2. **Batch API Implementation**

    - High business value
    - File upload/download handling
    - Skills needed: C#, file I/O, async programming

3. **Model Context Protocol (MCP)**
    - Cutting-edge feature
    - Complex integration
    - Skills needed: C#, protocol implementation, external API integration

### 🟡 Medium Priority

4. **Advanced Reasoning Features**

    - Extend existing reasoning support
    - Add format control and streaming
    - Skills needed: C#, API client development

5. **Compound System Built-in Tools**
    - Web search, browser automation, Wolfram Alpha
    - Skills needed: C#, tool integration, async programming

### 🟢 Good for Beginners

-   Documentation improvements
-   Code examples and tutorials
-   Unit tests for existing features
-   Code comments and XML documentation
-   Bug fixes

## 📖 Documentation Guidelines

### XML Documentation

All public APIs must have complete XML documentation:

```csharp
/// <summary>
/// Brief one-line description.
/// </summary>
/// <param name="paramName">Parameter description.</param>
/// <returns>Return value description.</returns>
/// <exception cref="ExceptionType">When this exception is thrown.</exception>
/// <remarks>
/// Additional details, usage examples, best practices.
/// Can include multiple paragraphs using <para> tags.
/// </remarks>
```

### README Updates

When adding new features:

-   Update the Features section
-   Add usage examples
-   Update the Table of Contents
-   Document any new configuration options

### Code Examples

-   Should be complete and runnable
-   Include necessary using statements
-   Show both basic and advanced usage
-   Include error handling examples

## 💬 Community

### Getting Help

-   **GitHub Issues** - For bugs and feature requests
-   **Discussions** - For questions and general discussion
-   **Groq Community** - [community.groq.com](https://community.groq.com)

### Asking Questions

Before asking a question:

1. Check existing issues and discussions
2. Read the README and documentation
3. Search the Groq documentation

When asking:

-   Be specific about your problem
-   Include code examples
-   Share error messages
-   Mention your environment (.NET version, OS)

### Reporting Bugs

Use the bug report template and include:

-   Clear, descriptive title
-   Steps to reproduce
-   Expected vs actual behavior
-   Code samples
-   Environment details
-   Error messages and stack traces

### Suggesting Features

Use the feature request template and include:

-   Clear description of the feature
-   Use cases and benefits
-   Proposed API design (if applicable)
-   Related Groq API documentation
-   Willingness to implement

## 🙏 Recognition

Contributors will be recognized in:

-   The project's contributors page
-   Release notes for significant contributions
-   README acknowledgements section

## 📄 License

By contributing, you agree that your contributions will be licensed under the same MIT License that covers the project.

---

**Thank you for contributing to GroqApiLibrary!** 🎉

Your contributions help make this library better for everyone in the .NET and AI community.
