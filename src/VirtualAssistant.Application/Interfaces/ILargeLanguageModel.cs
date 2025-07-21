namespace VirtualAssistant.Application.Interfaces
{
    public interface ILargeLanguageModel
    {
        Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken = default);
        Task<string> GenerateResponseAsync(string prompt, CancellationToken cancellationToken = default);
    }
}