using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using VirtualAssistant.Application.Interfaces;

namespace VirtualAssistant.Infrastructure.RAG;

public class OllamaService : ILargeLanguageModel
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OllamaService> _logger;
    private readonly string? _modelName;

    private record OllamaEmbeddingRequest(string model, string prompt);
    private record OllamaEmbeddingResponse(float[] embedding);
    private record OllamaGenerateRequest(string model, string prompt, bool stream = false);
    private record OllamaGenerateResponse(string response);


    public OllamaService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OllamaService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("Ollama");
        _logger = logger;
        _modelName = configuration["Ollama:Model"];

        if (string.IsNullOrEmpty(_modelName))
        {
            _logger.LogError("O nome do modelo do Ollama não está configurado em 'Ollama:Model'");
            throw new InvalidOperationException("Modelo do Ollama não configurado.");
        }
    }

    public async Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Gerando embedding para o texto: '{Text}'", text.Substring(0, Math.Min(text.Length, 50)));

        var request = new OllamaEmbeddingRequest(_modelName!, text);

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/embeddings", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var ollamaResponse = await response.Content.ReadFromJsonAsync<OllamaEmbeddingResponse>(cancellationToken: cancellationToken);
            return ollamaResponse?.embedding ?? Array.Empty<float>();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro ao chamar a API de embeddings do Ollama.");
            throw;
        }
    }

    public async Task<string> GenerateResponseAsync(string prompt, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Gerando resposta para o prompt: '{Prompt}'", prompt);

        var request = new OllamaGenerateRequest(_modelName!, prompt);

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/generate", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var ollamaResponse = await response.Content.ReadFromJsonAsync<OllamaGenerateResponse>(cancellationToken: cancellationToken);
            return ollamaResponse?.response ?? string.Empty;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro ao chamar a API de geração do Ollama.");
            throw;
        }
    }
}