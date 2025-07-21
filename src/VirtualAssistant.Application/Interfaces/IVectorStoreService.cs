using VirtualAssistant.Domain.Entities;

namespace VirtualAssistant.Application.Interfaces;

public interface IVectorStoreService
{
    Task<bool> CollectionExistsAsync(string collectionName, CancellationToken cancellationToken = default);
    Task CreateCollectionAsync(string collectionName, CancellationToken cancellationToken = default);
    Task AddDocumentsAsync(string collectionName, IEnumerable<DocumentChunk> chunks, IEnumerable<float[]> embeddings, CancellationToken cancellationToken = default);
    Task<IEnumerable<DocumentChunk>> SearchAsync(string collectionName, float[] queryVector, int limit = 5, CancellationToken cancellationToken = default);
}